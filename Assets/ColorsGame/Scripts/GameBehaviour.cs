using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ChooseColor.Game.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private int mistakesCount;
        [SerializeField] private Generator generator;
        [SerializeField] private List<Button> buttons;
        [SerializeField] private Transform layout;

        private int maxColorCount;

        void Start()
        {
            generator.GenerateLevel();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            //SetButtonsBehaviour(GetButtons(layout));
        }

        private List<Button> GetButtons(Transform layoutTransform)
        {
            List<Button> btns = new List<Button>();
            btns.AddRange(layoutTransform.GetComponentsInChildren<Button>());
            return btns;
        }

        private void SetButtonsBehaviour(List<Button> buttonsList)
        {
            foreach (var btn in buttonsList)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick
                    .AddListener(
                                    delegate 
                                    { 
                                        ButtonClicked(btn.name); 
                                    });
            }
        }

        private void ButtonClicked(string name)
        {
            Figure.Colors choosenColor;
            switch (name)
            {
                case "Red":
                    choosenColor = Figure.Colors.red;
                    break;
                case "Green":
                    choosenColor = Figure.Colors.green;
                    break;
                case "Blue":
                    choosenColor = Figure.Colors.blue;
                    break;
                case "Violet":
                    choosenColor = Figure.Colors.violet;
                    break;
                default:
                    choosenColor = default;
                    break;
            }

            CheckAnswer(choosenColor);
        }

        private void CheckAnswer(Figure.Colors choosenColor)
        {
            foreach (var item in SortFiguresByColor<KeyCount>())
            {
                if(item.Key.Equals(choosenColor))
                {
                    if(item.colorCount <= maxColorCount)
                    {

                    }
                }
            }
            throw new NotImplementedException();
        }

        internal IEnumerable<T> SortFiguresByColor<T>()
        {
            if (generator.figures.Count > 0)
            {
                var list = generator.figures.GroupBy(x => x.figureColor).Select(y => new { y.Key, colorCount = y.Count() });
                list = list.OrderByDescending(x => x.colorCount).ToList();
                maxColorCount = list.First().colorCount;

                return (IEnumerable<T>)list;
            }

            return new List<T>();
        }

        struct KeyCount
        {
            internal Figure.Colors Key;
            internal int colorCount;
        }
    }
}
