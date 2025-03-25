using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // [Header ("Patrol Points")]
    // [SerializeField] private Transform leftEdge;
    // [SerializeField] private Transform rightEdge;

    // [Header ("Enemy")]
    // [SerializeField] private Transform enemy;

    // [Header ("Movement parameters")]
    // [SerializeField] private float speed;
    // private Vector3 initScale;
    // private bool movingLeft;
    // // Start is called before the first frame update

    // [SerializeField] private Animator animator;
    // [Header ("Idle Behaviour")]
    // [SerializeField] private float idleDuration;
    // private float idleTimer;



    [SerializeField] private GameObject pointA;    
    [SerializeField] private GameObject pointB;
    [SerializeField] private Rigidbody2D rb; 
    // public Animator anim;
    private Transform currentPoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        // anim.SetBool("Moving",false);
    }

//
    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        } 
        else
        {
            rb. velocity = new Vector2(-speed, 0) ;
        }
        if(Vector2.Distance (transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            // anim.SetBool("Moving",true);
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA. transform)
        {
            flip();
            // anim.SetBool("Moving",true);
            currentPoint = pointB.transform;
        }
  
        
    }
    private void flip()
    {
        // anim.SetBool("Moving",false);
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDisable()
    {
        // anim.SetBool("Moving", false);
    }

}
