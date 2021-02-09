using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generator = ChooseColor.Game.Scripts.Generator;

namespace ChooseColor.Game.Scripts.NewScriptSystem
{
    public class GameBehaviour : MonoBehaviour
    {
        private Generator generator;
        private int[] colorsRepresentation;

        private void Start()
        {
            generator.GenerateLevel();
            colorsRepresentation = new int[Enum.GetValues(typeof(Figure.Colors)).Length];

            foreach (var item in generator.figures)
            {
            }
        }

        private void Update()
        {
        }
    }
}

