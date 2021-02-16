using System;
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

        internal static int enemiesCount;
        private Generator generator;

        private bool controlsEnabled = true;
        
        [SerializeField] private float timeToGeneration = 5.0f;
        private float generationTimer = 0.0f;

        private void Awake()
        {
            generationTimer = timeToGeneration;
            generator = GetComponent<Generator>();
            generator.Init(targetsArray, spawnPoint);
        }

        // Start is called before the first frame update
        void Start()
        {
            generator.Generate();
            enemiesCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(controlsEnabled)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            
            if(enemiesCount <= 0)
            {
                enemiesCount = 0;
                Debug.LogWarning(enemiesCount);
                controlsEnabled = false;
                if (generationTimer > 0)
                {
                    generationTimer -= Time.deltaTime;
                }
                else
                {
                    GenerateNewLevel();
                    generationTimer = timeToGeneration;
                }
            }
        }

        private void GenerateNewLevel()
        {
            generator.CleanLevel();
            generator.Generate();
            controlsEnabled = true;
        }
    }
}
