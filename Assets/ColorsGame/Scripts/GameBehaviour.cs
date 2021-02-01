using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChooseColor.Game.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private int mistakesCount;
        [SerializeField] private Generator generator;
        private Figure.Colors mostCommonColor;

        private int mostCommonColorCount;

        // Start is called before the first frame update
        void Start()
        {
            generator.GenerateLevel();
        }

        // Update is called once per frame
        void Update()
        {
            if (generator.figures.Count > 0)
            {
                var tmp = generator.figures.GroupBy(x => x.figureColor).Select(y => new { y.Key, mostCommonColorCount = y.Count() });

                //Debug.Log($"tmp = {tmp} count: {mostCommonColorCount}");
                //Debug.Log($"Most common: {tmp.Where(x=>)");
                foreach (var item in tmp)
                {
                    Debug.Log($"tmp = {item}");
                }
            }
        }
    }
}
