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

    public HealthBar healthBar;
    public int maxHealth = 100;
    int currentHealth;

    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    public GameTimer gameTimer;
    public SceneLoader sceneLoader;

    bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            }
            else if (movement.x > 0)
            {
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            }

            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        gameTimer.StopCountdown();
        StartCoroutine(LoadLooseSceneIn());
    }

    IEnumerator LoadLooseSceneIn()
    {
        yield return new WaitForSeconds(0.3f);

        sceneLoader.LoadLooseScene();
    }

    public void RestoreHealth(int health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public bool HealthIsFull()
    {
        return currentHealth == maxHealth;
    }
}
