using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds.Game.Scripts.Actor
{
    public class BirdBehaviour : MonoBehaviour
    {
        private bool isDragging = false;
        [SerializeField] private Rigidbody2D rBody;
        [SerializeField] private float secondsToRelease = 0.15f;

        private void Update()
        {
            if(isDragging)
            {
                rBody.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            }
        }

        private void OnMouseDown()
        {
            isDragging = true;
            rBody.isKinematic = true;
        }

        private void OnMouseUp()
        {
            isDragging = false;
            rBody.isKinematic = false;

            StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            yield return new WaitForSeconds(secondsToRelease);
            GetComponent<SpringJoint2D>().enabled = false;
        }
    }
}

