using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds.Game.Scripts.Actor
{
    public class BirdBehaviour : MonoBehaviour
    {
        private bool isDragging = false;
        [SerializeField] private Rigidbody2D rBody;
        [SerializeField] private Rigidbody2D rBodyHook;
        [SerializeField] private float secondsToRelease = 0.15f;
        [SerializeField] private float maxDragDistance = 2.0f;

        private void OnEnable()
        {
            Scene.SceneBehaviour.isProjectileOnScene = true;
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector3.Distance(mousePosition, rBodyHook.position) > maxDragDistance)
                {
                    rBody.position = rBodyHook.position + (mousePosition - rBodyHook.position).normalized * maxDragDistance;
                }
                else
                {
                    rBody.position = mousePosition;
                }
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
            this.enabled = false;
        }
    }
}

