using UnityEngine;
using UnityEditor;
using System.ComponentModel;

public class Grid {


    private int[,] cells;

    private int rows;
    private int cols;
    private int width;
    private int height;

    public Grid(int rows, int cols, int width, int height, bool debugird = true) {

        this.rows = rows;
        this.cols = cols;
        this.width = width;
        this.height = height;

        cells = new int[rows, cols];

        int index = 0;
        for (int i = 0; i < cells.GetLength(0); i++) {

            for (int j = 0; j < cells.GetLength(1); j++) {

                cells[i, j] = index;
                index++;

            }


        }

        if (debugird)
        {

            float timeduration = 1000;
            var color = Color.black;

           // Debug.DrawLine(Vector3.zero, new Vector3(60,10,52), color, timeduration);

            for (int x = 0; x < cells.GetLength(0); x++) {

                for (int y = 0; y < cells.GetLength(1); y++) {

                  
                    Debug.DrawLine(getPosition(x, y), getPosition(x, y + 1), color, timeduration);

                    Debug.DrawLine(getPosition(x, y), getPosition(x+1, y ), color, timeduration);

                }


            }

        }

    }


    public Vector3 getPosition(int x, int y) {


        return new Vector3(x*width, 0 , y* height);
    }


 




}