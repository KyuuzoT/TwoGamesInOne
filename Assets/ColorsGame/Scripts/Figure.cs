using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChooseColor.Game.Scripts
{
    [Serializable]
    public class Figure
    {
        internal enum Colors
        {
            red = 0,
            green = 1,
            blue = 2,
            violet = 3
        }

        [SerializeField] internal Colors figureColor { get; set; }
        [SerializeField] internal Transform figure;

        internal void ColorizeFigure(Color color)
        {
            figure.GetComponent<SpriteBehaviour>().SetColor(color);
        }

        internal void ColorizeFigure()
        {
            Color color = RelateToUnityColor();
            ColorizeFigure(color);
        }

        private Color RelateToUnityColor()
        {
            Color color;
            switch (figureColor)
            {
                case Colors.red:
                    color = Color.red;
                    break;
                case Colors.green:
                    color = Color.green;
                    break;
                case Colors.blue:
                    color = Color.blue;
                    break;
                case Colors.violet:
                    color = Color.magenta;
                    break;
                default:
                    color = Color.white;
                    break;
            }

            return color;
        }
    }
}
