using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds.Game.Scripts.Actor
{
    public class BirdBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D birdRB;
        [SerializeField] private float releaseTime { get; set; } = 0.01f;
        private bool isDragging { get; set; } = false;

        private void Update()
        {
            if (isDragging)
            {
                birdRB.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        private void OnMouseDown()
        {
            birdRB.isKinematic = true;
            isDragging = true;
        }

        private void OnMouseUp()
        {
            birdRB.isKinematic = false;
            isDragging = false;

            StartCoroutine(ReleaseBird());
        }

        private IEnumerator ReleaseBird()
        {
            yield return new WaitForSeconds(releaseTime);
            GetComponent<SpringJoint2D>().enabled = false;
        }
    }
}

