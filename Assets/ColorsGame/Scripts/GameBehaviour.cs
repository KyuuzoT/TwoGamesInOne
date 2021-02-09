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

        private SpriteRenderer[] figs;

        private int maxColorCount;
        private Transform container;
        private int buttonsClicked = 0;

        void Start()
        {
            generator.GenerateLevel();
            container = GameObject.FindGameObjectWithTag("Container").transform;
            figs = container.gameObject.GetComponentsInChildren<SpriteRenderer>();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            SetButtonsBehaviour(GetButtons(layout));

            if(buttonsClicked >= Enum.GetValues(typeof(Figure.Colors)).Length-1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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
            foreach (var item in SortFiguresByColor())
            {
                if(item.Key.Equals(choosenColor))
                {
                    if (item.Value >= maxColorCount)
                    {
                        foreach (var figure in figs)
                        {
                            Debug.Log($"Figure: {figure}");
                            if (figure.color.Equals(Figure.RelateToUnityColor(choosenColor)))
                            {
                                Destroy(figure.gameObject);
                            }
                        }
                        buttonsClicked++;
                    }
                    else
                    {
                        mistakesCount++;
                    }
                }
            }
            
            Debug.Log($"Mistakes: {mistakesCount}");
        }

        internal Dictionary<Figure.Colors,int> SortFiguresByColor()
        {
            Dictionary<Figure.Colors, int> dictr = new Dictionary<Figure.Colors, int>();
            if (generator.figures.Count > 0)
            {
                var dictionary = generator.figures.GroupBy(x => x.figureColor).Select(y => new { y.Key, colorCount = y.Count() });
                dictionary = dictionary.OrderByDescending(x => x.colorCount).ToList();
                maxColorCount = dictionary.First().colorCount;

                foreach (var item in dictionary)
                {
                    dictr.Add(item.Key, item.colorCount);
                }
            }

            return dictr;
        }

        struct KeyCount
        {
            internal Figure.Colors Key;
            internal int colorCount;
        }
    }
}
