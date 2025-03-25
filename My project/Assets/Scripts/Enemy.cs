using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealt = 100;
    int currentHealt;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Collider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;


    private EnemyPatrol enemyPatrol;
    [SerializeField] private PlayerCombat player;

    public float attackRange = 0.5f;
    public int attackDamage = 10;
    
    void Start()
    {
        currentHealt = maxHealt;
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Attack();
            }
        }
        //if (enemyPatrol != null)
        //    enemyPatrol.enabled = PlayerInSight();
    }
    void Attack()
    {
        animator.SetTrigger("MeeleAttack");
        player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        
    }
    public void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        animator.SetTrigger("Hurt");
        
        if(currentHealt <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log("Die");
        animator.SetBool("IsDead", true);
        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new UnityEngine.Vector3 (boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, UnityEngine.Vector2.left,0,playerLayer);


        return hit.collider != null;

    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos .DrawWireCube (boxCollider.bounds.center + transform.right * range * transform.localScale.x *colliderDistance, new UnityEngine.Vector3 (boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void PlayerDamage()
    {
        if(PlayerInSight())
        {
            TakeDamage(damage);
        }
    }


    

}
