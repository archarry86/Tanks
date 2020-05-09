using UnityEngine;
using UnityEditor;
using System.ComponentModel;

public class Grid {


    private int[,] matrix;

    public int[,] Matrix {
        get {
            return matrix;
        }
    }

    private int rows;
    private int cols;
    private int width;
    private int height;


    private Vector3 size = new Vector3();

    public Grid(int rows, int cols, int width, int height, bool debugird = true) {

        this.rows = rows;
        this.cols = cols;
        this.width = width;
        this.height = height;

        size = new Vector3(width,0, this.height);

        matrix = new int[rows, cols];

        int index = 0;
        for (int i = 0; i < matrix.GetLength(0); i++) {

            for (int j = 0; j < matrix.GetLength(1); j++) {

                matrix[i, j] = index;
                index++;

            }


        }

        if (debugird)
        {

            float timeduration = 1000;
            var color = Color.black;

           // Debug.DrawLine(Vector3.zero, new Vector3(60,10,52), color, timeduration);

            for (int x = 0; x < matrix.GetLength(0); x++) {

                for (int y = 0; y < matrix.GetLength(1); y++) {

                  
                    Debug.DrawLine(getPosition(x, y), getPosition(x, y + 1), color, timeduration);

                    Debug.DrawLine(getPosition(x, y), getPosition(x+1, y ), color, timeduration);


                    Debug.DrawLine(getPosition(x, y), getWorldPosition(x, y ), Color.red, timeduration);

                }


            }

        }

    }


    public Vector3 getPosition(int x, int y) {


        return new Vector3(x*width, 0 , y* height);
    }


    public Vector3 getWorldPosition(int x, int y) {


        return new Vector3(x * width, 0, y * height)+ (size * 0.5f);
    }

    //metodo que convierta las coordenadas del mouse en el mundo y luego se transforme a get world position





}