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
        [SerializeField] private Text mistakesCounterText;

        private List<SpriteRenderer> figs;

        private int maxColorCount { get; set; }
        private Transform container;
        private int buttonsClicked = 0;

        Dictionary<Figure.Colors, int> dictColors;

        void Start()
        {
            generator.GenerateLevel();
            container = GameObject.FindGameObjectWithTag("Container").transform;
            figs = container.gameObject.GetComponentsInChildren<SpriteRenderer>().ToList();
            dictColors = SortFiguresByColor();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            SetButtonsBehaviour(GetButtons(layout));
            mistakesCounterText.text = mistakesCount.ToString();
            if (buttonsClicked >= Enum.GetValues(typeof(Figure.Colors)).Length - 1)
            {
                foreach (var item in GetButtons(layout))
                {
                    item.enabled = false;
                }
                mistakesCount = 0;
                StartCoroutine(YieldLoadScene());
            }
        }

        private IEnumerator YieldLoadScene()
        {
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            Debug.Log($"Button: {name}");
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

        int i = 1;

        private void CheckAnswer(Figure.Colors choosenColor)
        {
            foreach (var item in dictColors)
            {
                if (item.Key.Equals(choosenColor))
                {
                    if (item.Value >= maxColorCount)
                    {
                        foreach (var figure in figs.ToList())
                        {
                            //Debug.LogWarning($"Figure: {figure}");
                            Debug.LogWarning($"1. Max: {maxColorCount}, item: {item.Value}");
                            if (figure.color.Equals(Figure.RelateToUnityColor(choosenColor)))
                            {
                                Destroy(figure.gameObject);
                                figs.Remove(figure);
                                Debug.Log("Destroyed!");
                            }
                        }
                        buttonsClicked++;
                        //maxColorCount = dictColors.Skip(i++).First().Value;
                        Debug.LogWarning($"2. Max: {maxColorCount}, item: {item.Value}, i: {i}");
                    }
                    else
                    {
                        mistakesCount++;
                    }
                }
            }

            if (i <= (Enum.GetValues(typeof(Figure.Colors)).Length - 1))
            {
                maxColorCount = dictColors.Skip(i++).First().Value;
            }
            Debug.Log($"Mistakes: {mistakesCount}");
            Debug.LogWarning($"3. Max: {maxColorCount}, i:{i}");
        }

        internal Dictionary<Figure.Colors, int> SortFiguresByColor()
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
