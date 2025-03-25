using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Update is called once per frame
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxHealt = 100;
    int currentHealt;
    public HealthBar playerHealth;
    
    void Start ()
    {
        currentHealt = maxHealt;
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        animator.SetTrigger("Hurt");
        playerHealth.SetHealth(currentHealt);
        if(currentHealt <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log("Die");
        animator.SetBool("IsDead", true);
        
        //GetComponent<Collider2D>().enabled = false;
        UIManager.Instance.ShowGameOverUI();
        this.enabled = false;

    }


}
