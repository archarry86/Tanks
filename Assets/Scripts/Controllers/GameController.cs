using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class GameController : MonoBehaviour {

    [SerializeField]
    private int Size = 25;

    [SerializeField]
    private int cellsize = 4;

 

    public GameObject spawnpoint1;


    public GameObject spawnpoint2;


    public GameObject spawnpoint3;


    public GameObject brick;

    public GameObject solid;


    public GameObject Player;

    public GameObject Enemy;

    public Button _GenerateScenario;


    public Dropdown Dropdown;

    public static Grid grind = null;
    // Start is called before the first frame update
    void Start() {
        var gridpos = Vector3.zero;// (Vector3.forward + Vector3.right) * cellsize;
        grind = new Grid(Size, Size , cellsize, gridpos , true);

        var creator = new SceneCreatorOne(Size, Size);

        CreateScenary(creator);



    }

    // Update is called once per frame
    void Update() {


    }



    public void GenerateScenario() {


        ////UnityEngine.//Debug.Log("GenerateScenario");


        int index = GetAlgorimtmindex();

        SceneCreator creator = null;

        switch (index) {

            case 0:

                creator = new SceneCreatorCharry(Size, Size);
                break;
            case 1:

                creator = new SceneCreatorPanqueva(Size, Size);
                break;
            default:
                creator = new SceneCreatorOne(Size, Size);
                break;

        }


        CreateScenary(creator);

    }

    private int GetAlgorimtmindex() {

        int val = -1;

        if (Dropdown != null)
            val = Dropdown.value;

        return val;
    }




    private void CleanBoard(SceneCreator creator) {

        int spawningpoints = 3;
        int _size = Size - 1;

        int sizethirdpart = _size / spawningpoints;

        int col = sizethirdpart - (sizethirdpart / 2);




        var spawingpoint = 5;
        for (int iterador = 0; iterador < spawningpoints; iterador++, col += sizethirdpart, spawingpoint++) {
            creator.SetCellValue(_size, col, spawingpoint);
            creator.SetCellValue(_size - 1, col, 0);
            creator.SetCellValue(_size - 2, col, 0);
            creator.SetCellValue(_size - 3, col, 0);
        }

        creator.SetCellValue(_size, sizethirdpart, 4);


        int halfboardpoint = _size / 2;

        int[] basepoints = { 0, 1, 0, 0, 0, 1, 0 };

        int[] endbasepoints = { 0, 1, 1, 1, 1, 1, 0 };

        int rowstosettheflag = 3;

        int i = 0;

        int halfpoints = (int)basepoints.Length / 2;

        if (halfpoints % 2 > 0) {
            halfpoints++;
        }

        int initualpoint = halfboardpoint - halfpoints;

        for (; i < rowstosettheflag; i++) {

            initualpoint = halfboardpoint - 3;

            foreach (int v in basepoints) {

                creator.SetCellValue(i, initualpoint, v);
                initualpoint++;
            }


        }

        initualpoint = halfboardpoint - 3;

        foreach (int v in endbasepoints) {

            creator.SetCellValue(i, initualpoint, v);
            initualpoint++;
        }


        i++;
        //siguiente fila
        initualpoint = halfboardpoint - 3;

        foreach (int v in basepoints) {

            creator.SetCellValue(i, initualpoint, 0);
            initualpoint++;
        }



        //ultima
        i++;
        initualpoint = halfboardpoint - 3;


        foreach (int v in basepoints) {

            creator.SetCellValue(i, initualpoint, 0);
            initualpoint++;
        }

        creator.SetCellValue(i, halfboardpoint, 3);

        //ultima
        i++;
        initualpoint = halfboardpoint - 3;


        foreach (int v in basepoints) {

            creator.SetCellValue(i, initualpoint, 0);
            initualpoint++;
        }





    }


    private void CreateScenary(SceneCreator creator) {


        var matrix = creator.Matrix;

        var list = GameObject.FindGameObjectsWithTag("Wall");


        foreach (var element in list) {

            GameObject.Destroy(element);
        }


        CleanBoard(creator);






        for (int rows = 0; rows < matrix.GetLength(0); rows++) {


            for (int columns = 0; columns < matrix.GetLength(1); columns++) {

                GameObject obj = null;
                var positionformcoordinates = grind.getGridWorldPosition(rows, columns);



                switch (matrix[rows, columns]) {

                    case 1:
                        obj = brick;
                        break;

                    case 2:
                        obj = solid;
                        break;
                    case 3:
                        if (Player != null) {
                      
                            Player.transform.position = positionformcoordinates;
                            var angles = Player.transform.eulerAngles;

                            angles.y = 0;

                            Player.transform.eulerAngles = angles;
                        }
                        break;
                    case 4:

                        if (Enemy != null) {
                         
                            Enemy.transform.position = positionformcoordinates;

                            var angles = Enemy.transform.eulerAngles;
                            angles.y = 180;

                            Enemy.transform.eulerAngles = angles;
                        }

                        break;
                    case 5:

                        if (this.spawnpoint1 != null) {

                            spawnpoint1.transform.position = positionformcoordinates;
                        }

                        break;
                    case 6:

                        if (this.spawnpoint2 != null) {
                            spawnpoint2.transform.position = positionformcoordinates;
                        }
                        break;
                    case 7:

                        if (this.spawnpoint3 != null) {
                            spawnpoint3.transform.position = positionformcoordinates;
                        }
                        break;


                }

                if (obj != null) {
                    positionformcoordinates = grind.getGridWorldPosition(rows, columns);
                    positionformcoordinates.y = obj.transform.position.y;

                    var wall = GameObject.Instantiate(obj, positionformcoordinates, Quaternion.identity);

                    wall.tag = "Wall";
                }

            }




        }


    }
}
