using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{

    public int scale = 5;
    //public bool showdirectionsongame = false;
    // Start is called before the first frame update
    void Start()
    {
    
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        var vectoraux = this.transform.position;
        vectoraux.y = vectoraux.y - vectoraux.y;

        Gizmos.DrawRay(vectoraux, ( transform.forward * scale));

        Gizmos.color = Color.blue;
    

        Gizmos.DrawRay(vectoraux, (transform.right * scale));

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(vectoraux, (transform.up * scale));


    }
}
