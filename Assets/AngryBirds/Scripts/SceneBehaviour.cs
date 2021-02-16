using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AngryBirds.Game.Scripts.Scene
{
    public class SceneBehaviour : MonoBehaviour
    {
        [SerializeField] Transform[] targetsArray;
        [SerializeField] Transform spawnPoint;

        private Generator generator;

        private void Awake()
        {
            generator = GetComponent<Generator>();
            generator.Init(targetsArray, spawnPoint);
        }

        // Start is called before the first frame update
        void Start()
        {
            generator.Generate();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
