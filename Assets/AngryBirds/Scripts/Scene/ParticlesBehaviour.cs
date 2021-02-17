using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBehaviour : MonoBehaviour
{
    void Update()
    {
        if(gameObject.GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(gameObject);
        }
    }
}
