using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AngryBirds.Game.Scripts.Actor
{
    public class DestroyProjectile : MonoBehaviour
    {
        [SerializeField] private Transform deathEffect;
        [SerializeField] private float timeToDestroy = 3.0f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject, timeToDestroy);
        }

        private void OnDestroy()
        {
            var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = gameObject.GetComponent<Renderer>().sortingOrder;
        }
    }
}