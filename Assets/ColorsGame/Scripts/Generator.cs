using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private const int ROWS = 5;
    private const int COLS = 10;
    private const float CELL_SIZE = 1.5f;
    private int[,] figsMatrix;

    [SerializeField]GameObject figsPrefab;

    private void Start()
    {
        figsMatrix = new int[ROWS, COLS];

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                figsMatrix[i, j] = Random.Range(0, 2);
            }
        }
        InstantiateFigures();
    }

    void InstantiateFigures()
    {

    }
}
