using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject brick;
    public GameObject solid;
    // Start is called before the first frame update
    void Start() {
        Grid grind = new Grid(25, 25, 4, 4);


        SceneCreator creator = new SceneCreatorCharry(25, 25);

        Debug.Log(creator.ToString());

        var matrix = creator.Matrix;


        for (int rows = 0; rows < matrix.GetLength(0); rows++) {


            for (int columns = 0; columns < matrix.GetLength(1); columns++) {

                GameObject obj = null;


                switch (matrix[rows, columns]) {


                    case 1:
                        obj = brick;
                        break;

                    case 2:
                        obj = solid;
                        break;
                }
                if (obj != null) {
                    var pos = grind.getWorldPosition(rows, columns);
                    pos.y = obj.transform.position.y;

                    GameObject.Instantiate(obj, pos, Quaternion.identity);
                }
            }

        }



        /*
        creator = new SceneCreator(25, 25);
        Debug.Log("second matrix");
        Debug.Log(creator.ToString());
        creator = new SceneCreator(25, 25);
        Debug.Log("third matrix");
        Debug.Log(creator.ToString());*/
    }

    // Update is called once per frame
    void Update() {

    }
}
