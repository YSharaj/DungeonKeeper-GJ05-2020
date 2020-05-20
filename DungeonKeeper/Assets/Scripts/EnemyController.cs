using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public int attackDamage = 15;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    SpriteRenderer spriteRenderer;
    Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        GetComponent<AIDestinationSetter>().target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Time.time >= nextAttackTime)
            {
                Attack(collision.gameObject);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack(GameObject player)
    {
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        // Play hurt animation
        spriteRenderer.color = Color.red;
        StartCoroutine(BlinkRed());

        if (currentHealth <= 0) {
            Die();
        }
    }

    IEnumerator BlinkRed()
    {
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.3f);

        //After we have waited 5 seconds print the time again.
        spriteRenderer.color = currentColor;
    }

    void Die() {
        Destroy(gameObject, 0.3f);
    }
}
