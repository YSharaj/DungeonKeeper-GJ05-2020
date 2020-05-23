using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int healingPower = 15;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (!player.HealthIsFull())
            {
                player.RestoreHealth(healingPower);
                Destroy(gameObject);
            }
        }
    }
}
