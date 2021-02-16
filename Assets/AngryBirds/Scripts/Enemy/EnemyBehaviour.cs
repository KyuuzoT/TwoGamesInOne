using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds.Game.Scripts.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private float health = 4.0f;
        [SerializeField] private Transform deathEffect;

        private void Start()
        {
            Scene.SceneBehaviour.enemiesCount++;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log($"Force: {collision.relativeVelocity.magnitude}");
            if (collision.relativeVelocity.magnitude > health)
            {
                Die();
            }
        }

        private void Die()
        {
            var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = gameObject.GetComponent<Renderer>().sortingOrder;

            Scene.SceneBehaviour.enemiesCount--;

            Destroy(gameObject);
        }
    }
}
