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

    public int level = 1;
    public double xp = 0;
    private double upXp = 100;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange;
    public int attackDamage;
    public int absorbDamage = 10;
    public float currentStamina;
    public float maxStamina;
    public float maxHealth;
    public int takedamage;
    public float currentHealth;
    public bool isdead = false;
    public static bool isMouseInputEnabled = true;
    private bool moving;

    public int noOfClicks = 0;
    float lastClickledTime = 0;
    public float maxComboDelay = 0.9f;

    public StaminaBar staminaBar;
    public HealthBar healthBar;
    public GameObject skillPanel;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        healthBar.SetHealth(currentHealth);
      
        staminaBar.SetStamina(currentStamina);
        if (currentStamina < maxStamina)
        {
            currentStamina +=  2 * Time.deltaTime;
        }


        if (Time.time - lastClickledTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetMouseButton(1))
        {
            if(currentStamina >= 10) { 
            animator.SetBool("BlockIdle", true);
            }
        }
        else
        {
            animator.SetBool("BlockIdle", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(currentStamina >= 10) {
                noOfClicks++;
                lastClickledTime = Time.time;

            if (noOfClicks == 1 && isMouseInputEnabled == true)
            {
                animator.SetBool("Attack1", true);
                currentStamina = currentStamina - 10;
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
            }
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
            upXp += upXp*1.1;
            skillPanel.GetComponent<TriggerPanel>().openPanel();
        }
    }
    public void GainXp(int gainXp)
    {
        isMouseInputEnabled = true;
        xp += gainXp;
    }
    public void GainAttackDamage()
    {
        skillPanel.GetComponent<TriggerPanel>().closePanel();
        isMouseInputEnabled = true;
        attackDamage += 10;
    }
    public void GainBlockDamage()
    {
        skillPanel.GetComponent<TriggerPanel>().closePanel();
        isMouseInputEnabled = true;
        absorbDamage += 10;
        
    }
    public void GainHP()
    {
        isMouseInputEnabled = true;
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
            currentStamina = currentStamina - 10;
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
            currentStamina = currentStamina - 10;
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
            currentStamina = currentStamina - 5;
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