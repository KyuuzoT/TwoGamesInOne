using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AngryBirds.Game.Scripts.Actor
{
    public class DestroyProjectile : MonoBehaviour
    {
        [SerializeField] private Transform deathEffect;
        [SerializeField] private float timeToDestroy = 3.0f;
        private Transform effectInstance;
        private float timer = 0;

        private bool isHit = false;

        private void Awake()
        {
            timer = timeToDestroy;
        }

        private void Update()
        {
            if(isHit)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }

                if (timer <= 0)
                {
                    PlayDeathEffect();
                    timer = timeToDestroy;
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }
        }

        private void PlayDeathEffect()
        {
            effectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effectInstance.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = gameObject.GetComponent<Renderer>().sortingOrder;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            isHit = true;           
        }

        private void OnDestroy()
        {
            Scene.SceneBehaviour.isProjectileOnScene = false;
        }
    }
}