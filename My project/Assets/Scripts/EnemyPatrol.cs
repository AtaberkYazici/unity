using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private float idleDuration = 1f; // Bekleme s�resi
    [SerializeField] private float threshold = 0.3f;  // Mesafe kontrol e�i�i

    private Transform currentPoint;
    private bool isIdle = false;

    private void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!animator) animator = GetComponent<Animator>();

        currentPoint = pointB;
    }

    private void Update()
    {
        if (!isIdle)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        // Hedefe do�ru y�n belirle ve hareket et
        Vector2 direction = (currentPoint.position - rb.transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        animator.SetBool("Moving", true);

        // Mesafe e�i�ine g�re hedefe ula��ld� m� kontrol et
        if (Vector2.Distance(rb.transform.position, currentPoint.position) < threshold)
        {
            StartCoroutine(IdleRoutine());
        }
    }

    private IEnumerator IdleRoutine()
    {
        isIdle = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("Moving", false);
        yield return new WaitForSeconds(idleDuration);
        Flip();
        // Hedefi de�i�tir
        currentPoint = currentPoint == pointB ? pointA : pointB;
        isIdle = false;
    }

    private void Flip()
    {
        // Y�n de�i�tirmek i�in scale ters �evrilir
        Vector3 scale = rb.transform.localScale;
        scale.x *= -1;
        rb.transform.localScale = scale;
    }

    private void OnDisable()
    {
        animator.SetBool("Moving", false);
    }
}
