using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemyBehavior : MonoBehaviour
{
    //movement limit
    public float distance;
    public float mindistance;

    //combat
    public int maxHealth = 100;
    int currentHealth;
    public LayerMask playerLayer;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int xpGain;
    public int takedamage;

    //imports
    public Transform player;
    
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private Sensor_Bandit m_groundSensor;

    public HealthBar healthBar;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetVisible(false);
    }

    void Update()
    {
        if (player.GetComponent<PlayerCombat>().isdead == false) {
            if (Vector2.Distance(transform.position, player.position) > 10)
            {
                animator.SetInteger("AnimState", 0);
                Vector3 direction = Vector3.zero;
                direction.Normalize();
                movement = direction;
            }
            if (Vector2.Distance(transform.position, player.position) <= distance)
            {
                chase();
                changeposition();
            }
            if (Vector2.Distance(transform.position, player.position) <= mindistance)
            {
                changeposition();
                animator.SetInteger("AnimState", 1);
                if (Time.time >= nextAttackTime)
                {
                    if(Vector2.Distance(transform.position, player.position) <= distance)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else
                    {
                        animator.SetInteger("AnimState", 0);
                    }
                    nextAttackTime = Time.time + 2f / attackRate;
                }   
            }
        }
        else
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }
    }

    void FixedUpdate()
    {
    moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && Vector2.Distance(transform.position, player.position) >= mindistance)
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void chase()
    {
        animator.SetInteger("AnimState", 2);
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }
 
    private void attack() {

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D Player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().TakeHit(attackDamage); 
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void changeposition()
    {
        if (transform.position.x <= player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void TakeDamage(int damage)
    {
        takedamage = damage;
        animator.SetTrigger("Hurt");
    }
    public void TakeRealDamage()
    {
        healthBar.SetVisible(true);
        currentHealth -= takedamage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        player.GetComponent<PlayerCombat>().GainXp(xpGain);
        animator.SetBool("IsDead",true);
        GetComponent<Collider2D>().enabled = false;
        healthBar.SetVisible(false);
        this.enabled = false;
        
    }
}



