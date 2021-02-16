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
        [SerializeField] private int triesCount = 3;
        private int tries;

        private Generator generator;
        [SerializeField] private float timeToGeneration = 5.0f;
        private float generationTimer = 0.0f;

        private bool controlsEnabled = true;

        internal static int enemiesCount;

        internal static bool isProjectileOnScene = false;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private Transform projectile;

        private void Awake()
        {
            generationTimer = timeToGeneration;
            generator = GetComponent<Generator>();
            generator.Init(targetsArray, spawnPoint);
            tries = triesCount;
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
                LevelGeneration();
            }

            if(!isProjectileOnScene)
            {
                if(triesCount > 0)
                {
                    GenerateNewProjectile();
                    triesCount--;
                }
            }
        }

        private void GenerateNewProjectile()
        {
            Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
        }

        private void LevelGeneration()
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

        private void GenerateNewLevel()
        {
            generator.CleanLevel();
            generator.Generate();
            controlsEnabled = true;
            triesCount = tries;
        }
    }
}
