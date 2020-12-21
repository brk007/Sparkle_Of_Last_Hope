using System;
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

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange;
    public int attackDamage;
    public int maxHealth;
    public int takedamage;
    public int currentHealth;
    public bool isdead = false;
    public bool attacking = false;
    private bool moving;

    public HealthBar healthBar;

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

        if (Input.GetMouseButtonDown(0))
        {
            moving = false;
            animator.SetTrigger("Attack");
        }
        if ((Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("d") | Input.GetKey("s")) && Input.GetMouseButtonDown(0) == false)
        {
            if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                moving = true;
        }
        else
        {
            animator.SetInteger("AnimState", 0);
            m_body2d.velocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        if (moving) { 
            animator.SetInteger("AnimState", 2);
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

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        takedamage = damage;
        animator.SetTrigger("Hurt");
    }
    public void TakeRealDamage()
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