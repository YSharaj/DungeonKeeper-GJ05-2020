using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        // Play hurt animation
        StartCoroutine(BlinkRed());

        if (currentHealth <= 0) {
            Die();
        }
    }

    IEnumerator BlinkRed()
    {
        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.3f);

        //After we have waited 5 seconds print the time again.
        spriteRenderer.color = currentColor;
    }

    void Die() {
        Destroy(gameObject, 0.3f);
    }
}
