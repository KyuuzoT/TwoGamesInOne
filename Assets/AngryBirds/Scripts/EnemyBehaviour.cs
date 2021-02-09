using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float health = 4.0f;
    [SerializeField] private Transform deathEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > health)
        {
            Die();
        }
        Debug.Log(collision.relativeVelocity.magnitude);
    }

    private void Die()
    {
        Instantiate(deathEffect);
        Destroy(gameObject,0.5f);
    }
}
