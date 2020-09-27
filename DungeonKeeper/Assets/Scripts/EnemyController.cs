﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public string enemyName;

    public int maxHealth = 100;
    int currentHealth;

    public int attackDamage = 15;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    public GameObject healingPotionPrefab;
    public int dropChanceInPercents = 25;

    public bool isHurtAnimation = true;

    public GameObject deathVFX;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Color currentColor;

    bool isAttacking = false;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentColor = spriteRenderer.color;
        animator = GetComponentInChildren<Animator>();

        GetComponent<AIDestinationSetter>().target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            if (Time.time >= nextAttackTime && player != null)
            {
                Attack(player);
                nextAttackTime = Time.time + 1f / attackRate;
            } else {
                animator.ResetTrigger("Attack");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    void Attack(GameObject player)
    {
        if (enemyName == "Demon") {
            FindObjectOfType<AudioManager>().Play("Bite");
		} else if (enemyName == "Skeleton") {
            FindObjectOfType<AudioManager>().Play("MegaSlash");
        }
        animator.SetTrigger("Attack");
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (!isHurtAnimation) {
            //spriteRenderer.color = Color.red;
            //StartCoroutine(BlinkRed());
		} else  {
            animator.SetTrigger("Hurt");
		}

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
        DropItemsAfterDeath();
        Destroy(gameObject);
        //Vector3 explosionVector = new Vector3(transform.position.x, transform.position.y, 0f);
        GameObject deathExplosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(deathExplosion, 1f);
    }

    void DropItemsAfterDeath()
    {
        float chance = Random.Range(0f, 100f);
        if (chance <= dropChanceInPercents)
        {
            Instantiate(healingPotionPrefab, transform.position, Quaternion.identity);
        }
    }
}
