using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AngryBirds.Game.Scripts.Actor
{
    public class DestroyProjectile : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 3.0f;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}