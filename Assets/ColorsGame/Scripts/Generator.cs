using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChooseColor.Game.Scripts
{
    public class Generator : MonoBehaviour
    {
        private const int ROWS = 5;
        private const int COLS = 10;
        private const float CELL_SIZE = 1.5f;
        private int[,] figsMatrix;

        [SerializeField] private Transform[] figsPrefabs;
        [SerializeField] private Vector3 cameraOffset;
        [SerializeField][Range(0.0f, 255.0f)] private float guiOffset = 3.0f;
        [SerializeField] private List<Figure> figures = new List<Figure>();

        private void Start()
        {
            figsMatrix = new int[ROWS, COLS];
            cameraOffset = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x + guiOffset, Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y + guiOffset);

            FillMatrixMap();
            InstantiateFigures();
        }

        private void Update()
        {

        }

        private void FillMatrixMap()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    figsMatrix[i, j] = Random.Range(0, 2);
                }
            }
        }

        void InstantiateFigures()
        {
            int index = 0;
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    index = Random.Range(0, figsPrefabs.Length);
                    if (figsMatrix[i, j] > 0)
                    {
                        var instance = Instantiate(figsPrefabs[index]);
                        Debug.Log($"Instance #{i},{j} - {instance}");
                        AddToList(instance);

                        Vector3 randomOffset = new Vector3(Random.Range(0.0f, 0.25f), Random.Range(0.0f, 0.25f));
                        instance.transform.position = new Vector3(CELL_SIZE * j, CELL_SIZE * i) + randomOffset;
                        instance.transform.position += cameraOffset;

                        SwapThisAndLastInArray(index);
                    }
                }
            }
        }

        private void AddToList(Transform instance)
        {
            Debug.Log($"Current instance: {instance}");
            var figure = new Figure();
            figure.figure = instance;
            figure.figureColor = (Figure.Colors)Random.Range(0, 5);
            figure.ColorizeFigure();
            figures.Add(figure);
        }

        private void SwapThisAndLastInArray(int index)
        {
            Transform tmpFigure = figsPrefabs[figsPrefabs.Length - 1];
            figsPrefabs[figsPrefabs.Length - 1] = figsPrefabs[index];
            figsPrefabs[index] = tmpFigure;
        }
    }

}