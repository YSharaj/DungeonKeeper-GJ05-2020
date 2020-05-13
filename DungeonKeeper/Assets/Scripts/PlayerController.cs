using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;

    public Rigidbody2D rb;
    public Animator animator;

    public Transform attackPoint;
    public int attackDamage = 25;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0) {
            transform.localScale = new Vector3 (-1f, transform.localScale.y, transform.localScale.z);
        } else if (movement.x > 0) {
            transform.localScale = new Vector3 (1f, transform.localScale.y, transform.localScale.z);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack() {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
