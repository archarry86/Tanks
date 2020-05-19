using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridTestingController : MonoBehaviour {


    private string TestTag = "TestTag";

    public GameObject tile;

    public GameObject objectToClone;


    private GameObject[] pool = new GameObject[8];


    public enum MethodToTest {
        none,
        GetSorroundPositions,
        GetSorroundWorldPositions,
    }


    [SerializeField]
    private MethodToTest method = MethodToTest.GetSorroundPositions;
    // Use this for initialization

    private Vector3 positiontohide = Vector3.zero;
    void Start() {

        positiontohide = Camera.main.transform.position + Camera.main.transform.right * 2;

        if (objectToClone != null) {

            for (int i = 0; i < pool.Length; i++) {
                pool[i]=Object.Instantiate(objectToClone, positiontohide, Quaternion.identity);

                pool[i].SetActive(false);
            }

        }

    }

    // Update is called once per frame
    void Update() {



       /* if (Input.GetMouseButton(0) || Input.GetMouseButton(0))*/ {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayHit = new RaycastHit();

            if (Physics.Raycast(ray, out RayHit, LayerMask.GetMask("terrain"))) {

                var ObjectHit = RayHit.transform.gameObject;
                if (ObjectHit != null) {


                    var Hitpoint = RayHit.point;
                    if (tile != null) {
                       var newposition  = GameController.grind.getGridPositionFromWorldPosition(Hitpoint);
                        /*if (tile.transform.position != newposition)*/ {
                            tile.transform.position = newposition;
                        Vector3[] positions = null;
                        switch (method) {
                            case MethodToTest.GetSorroundWorldPositions:
                                positions =   GameController.grind.GetSorroundWorldPositions(tile.transform.position);
                               
                                break;
                            case MethodToTest.GetSorroundPositions:

                                positions = GameController.grind.GetSorroundPositions (tile.transform.position);

                                break;
                        }


                            GetObjectsInPositions(ref positions);

                        }


                    }

                    UnityEngine.Debug.DrawLine(Camera.main.transform.position, Hitpoint, Color.blue, 0.5f);
                }


            }
        }
        /*
        else {

            tile.transform.position = Camera.main.transform.position + Camera.main.transform.right * 2;
        }*/


    }


    public void GetObjectsInPositions(ref Vector3[] calculatedpositions)
    {
       

        int initial = calculatedpositions.Length;
        int j = 0;
        foreach (var gameobj in pool.Take(initial)) {
            gameobj.SetActive(true);
             gameobj.transform.position = calculatedpositions[j];
            j++;
           
        }

        for (int i = initial; i < pool.Length; i++) {
            pool[i].transform.position = positiontohide;
            pool[i].SetActive(false);

        }


    }



}
