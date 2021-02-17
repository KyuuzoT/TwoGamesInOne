using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace AngryBirds.Game.Scripts.Scene
{
    public class Generator : MonoBehaviour
    {
        Transform[] targets;
        Transform spawnPoint;
        Transform targetInstance;

        internal void Init(Transform[] targerts, Transform spawnPoint)
        {
            this.targets = targerts;
            this.spawnPoint = spawnPoint;
        }

        internal void Generate()
        {
            if (targets.Length > 0 && !spawnPoint.Equals(null))
            {
                int index = Random.Range(0, targets.Length);
                Debug.LogWarning($"Index: {index}");
                targetInstance = Instantiate(targets[index], spawnPoint.position, Quaternion.identity);
            }
        }

        internal void CleanLevel()
        {
            Destroy(targetInstance.gameObject);
        }
    }
}
