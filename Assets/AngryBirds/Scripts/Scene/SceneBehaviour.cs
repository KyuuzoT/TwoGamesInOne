using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AngryBirds.Game.Scripts.Scene
{
    public class SceneBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI triesNumber; 
        [SerializeField] private TextMeshProUGUI enemiesNumber; 
        [SerializeField] private Transform[] targetsArray;
        [SerializeField] private Transform spawnPoint;
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
            controlsEnabled = true;
            tries = triesCount;
        }

        // Start is called before the first frame update
        void Start()
        {
            generator.Generate();
            GenerateNewProjectile();
            isProjectileOnScene = true;
            enemiesCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                if (controlsEnabled)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                if (enemiesCount <= 0)
                {
                    LevelGeneration();
                }
            }
            
            if (!isProjectileOnScene)
            {
                if (triesCount > 0)
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

        private void OnGUI()
        {
            enemiesNumber.text = enemiesCount.ToString();
            triesNumber.text = triesCount.ToString();
        }
    }
}
