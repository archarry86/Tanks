using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SceneCreator {



    public virtual int[,] Matrix {
        get {
            return null;
        }
    }


    public void SetCellValue(int row, int col, int value  ) {

        if (Matrix != null) {
            //Debug.Log("SetCellValue " + row + " " + col);
            Matrix[row, col] = value;
        }
    }


    public override string ToString() {
        StringBuilder bld = new StringBuilder();
        for (int rows = Matrix.GetLength(0) - 1; rows > -1; rows--) {


            var val = Matrix[rows, 0];
            bld.Append(val);

            for (int columns = 1; columns < Matrix.GetLength(1); columns++) {

                val = Matrix[rows, columns];
                bld.Append("\t" + val);

            }


            bld.AppendLine();

        }

        return bld.ToString();

    }

}

public class SceneCreatorOne : SceneCreator {

    private int[,] matrix;
    public override int[,] Matrix {
        get {
            return matrix;
        }
    }

    public SceneCreatorOne(int rows, int cols) {

        matrix = new int[rows, cols];
        matrix[0, 0] = 1;


        matrix[0, cols - 1] = 1;

        matrix[rows-1, cols-1] = 2;
        // matrix[rows - 1, cols - 2] = 2;
        // matrix[rows - 1, 1] = 2;
         matrix[rows - 1, 0] = 2;

    }



}


public class SceneCreatorCharry : SceneCreator {


    private int[,] matrix;

    public override int[,] Matrix {
        get {
            return matrix;
        }
    }

    public SceneCreatorCharry(int rows, int cols) {

        matrix = new int[rows, cols];
        Generate();
    }

    //se llena la matrix;
    private void Generate() {
        //guardarla cuando se tengan algoritmos bien definidos
        //Random.seed = 0;

        //por ahora

        int[] walls = new int[] { 
            (int)WallType.brick, 
            (int)WallType.solid };


        List<int> columns = Enumerable.Range(0, numberofcolumns).ToList();

        //Debug.Log(" columns " + string.Join(",", columns.ToArray()).ToString());
        bool inprocess = true;
        //Debug.Log(" Start " + columns.Count);
        var indexcolumn = 0;
        while (inprocess) {

            int icols = Random.Range(0, columns.Count);
            indexcolumn = columns[icols];


            bool inprocesscol = true;

            var method = Random.Range(0, 3);



            var wall = Random.Range(0, walls.Length) + 1;

            var opositewall = wall == 1 ? 2 : 1;

            int numberofwalls = 0;



            if (wall == 0)
                throw new System.Exception("invalid wall");

            while (inprocesscol) {


                var amount = (int)Random.Range(1, numberofrows * 0.33f);// numberofrows * 0.33f);

                int indexrow = (int)Random.Range(numberofwalls, numberofwalls + 2);

                numberofwalls += indexrow;

                //Debug.Log(" indexcolumn " + indexcolumn + " indexrow " + indexrow);

                switch (method) {

                    case 0:
                        //asing the amount of the wall type

                        for (int i = 0; i < amount && indexrow < numberofrows; i++, indexrow++) {

                            matrix[indexrow, indexcolumn] = wall;
                            numberofwalls++;



                        }
                        //empty fields
                        numberofwalls += amount;
                        indexrow += amount;
                        //add the index row


                        break;
                    case 1:
                        //de a y y luego random otro tipo, random para siguiente fila,  otro para cantidad

                        for (int i = 0; i < amount && indexrow < numberofrows; i++, indexrow++) {


                            matrix[indexrow, indexcolumn] = wall;
                            numberofwalls++;

                        }
                        var secondamount = (int)Random.Range(1, 3);


                        for (int i = 0; i < secondamount && indexrow < numberofrows; i++, indexrow++) {


                            numberofwalls++;
                            indexrow++;

                        }

                         secondamount = (int)Random.Range(0, 4);

                        for (int i = 0; i < (secondamount) && indexrow < numberofrows; i++, indexrow++) {

                            if (indexrow < numberofrows) {
                                matrix[indexrow, indexcolumn] = opositewall;
                                numberofwalls++;
                            }

                        }




                        break;
                    case 2:
                        //agrega la cantidad luego de a uno vacio y luego del otro tipo


                        for (int i = 0; i < amount && indexrow < numberofrows; i++, indexrow++) {


                            matrix[indexrow, indexcolumn] = wall;
                            numberofwalls++;

                        }

                        numberofwalls++;
                        indexrow++;

                        secondamount = (int)Random.Range(0, 3);

                        for (int i = 0; i < secondamount && indexrow < numberofrows; i++, indexrow++) {


                            matrix[indexrow, indexcolumn] = opositewall;
                            numberofwalls++;


                        }


                        break;

                    default:

                        throw new System.Exception("ERROR");

                        break;

                }


                inprocesscol = numberofwalls < numberofrows;


            }

            //columns.Remove(indexcolumn);
            columns.RemoveAt(icols);

            inprocess = columns.Count > 0;

            indexcolumn++;
        }

        //Debug.Log(" end " + columns.Count);

    }


    private int numberofrows => matrix.GetLength(0);
    private int numberofcolumns => matrix.GetLength(1);






}



public class SceneCreatorPanqueva : SceneCreator {


    private int[,] matrix;

    public override int[,] Matrix {
        get {
            return matrix;
        }
    }

    public SceneCreatorPanqueva(int rows, int cols) {

        matrix = new int[rows, cols];
        Generate();
    }

    //se llena la matrix;
    private void Generate() {
        //guardarla cuando se tengan algoritmos bien definidos
        //Random.seed = 0;



        for (int rows = 0; rows < matrix.GetLength(0); rows++) {


            for (int columns = 1; columns < matrix.GetLength(1); columns++) {

                var random = Random.Range(0, 100);

                if (random < 25f) {
                    matrix[rows, columns] = 2;
                }
                else if (random < 60f) {
                    matrix[rows, columns] = 1;
                }
                else {
                    matrix[rows, columns] = 0;
                }

            }

        }
    }


    private int numberofrows => matrix.GetLength(0);
    private int numberofcolumns => matrix.GetLength(1);


   



}