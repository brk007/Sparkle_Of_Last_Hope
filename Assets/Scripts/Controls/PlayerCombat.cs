﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D m_body2d;
    public float speed;

    public int level = 1;
    public int xp = 0;
    private int upXp = 100;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange;
    public int attackDamage;
    public int absorbDamage = 10;
    public int maxHealth;
    public int takedamage;
    public int currentHealth;
    public bool isdead = false;
    public static bool isMouseInputEnabled = true;
    private bool moving;

    public int noOfClicks = 0;
    float lastClickledTime = 0;
    public float maxComboDelay = 0.9f;

    public HealthBar healthBar;
    public GameObject skillPanel;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        if (Time.time - lastClickledTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("BlockIdle", true);
        }
        else
        {
            animator.SetBool("BlockIdle", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastClickledTime = Time.time;
            noOfClicks++;

            if (noOfClicks == 1 && isMouseInputEnabled == true)
            {
                animator.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }

        if ((Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("d") | Input.GetKey("s")) && Input.GetMouseButtonDown(0) == false)
        {
            if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                moving = true;
        }
        else
        {
            animator.SetBool("Run", false);
            m_body2d.velocity = Vector3.zero;
        }

        if(xp >= upXp)
        {
            level += 1;
            xp = 0;
            upXp += 50;
            skillPanel.GetComponent<TriggerPanel>().openPanel();
        }
    }
    public void GainXp(int gainXp)
    {
        xp += gainXp;
    }
    public void GainAttackDamage()
    {
        skillPanel.GetComponent<TriggerPanel>().closePanel();
        attackDamage += 10;
    }
    public void GainBlockDamage()
    {
        skillPanel.GetComponent<TriggerPanel>().closePanel();
        absorbDamage += 10;
        
    }
    public void GainHP()
    {
        skillPanel.GetComponent<TriggerPanel>().closePanel();
        maxHealth += 20;
        
    }
    void FixedUpdate()
    {
        if (moving)
        {
            animator.SetBool("Run", true);
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            if (moveHorizontal > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (moveHorizontal < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            m_body2d.AddForce(movement * speed);
        }
    }

    void Attack()
    { 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBehavior>().TakeDamage(attackDamage);
        }
    }
    void return1()
    {
        if (noOfClicks >= 2)
        {
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }
    void return2()
    {
        if (noOfClicks >= 3)
        {
            animator.SetBool("Attack3", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }
    void return3()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        noOfClicks = 0;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeHit(int damage)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BlockIdle"))
        {
            takedamage = damage - absorbDamage;
            animator.SetBool("BlockIdle", false);
            animator.SetTrigger("Block");
        }
        else
        {
            takedamage = damage;
            animator.SetTrigger("Hurt");
        }
    }

    public void TakeDamage()
    {
        currentHealth -= takedamage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        isdead = true;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.currentHealth;
        attackDamage = data.attackDamage;
        attackRange = data.attackRange;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}