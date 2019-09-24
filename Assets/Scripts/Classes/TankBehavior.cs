using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    Rigidbody rigidbody;
    public float movementSpeed = 20f;

    public bool invertupdowndir = false;

    bool IsButtonMovingPressed = false;

    float tiltAngle = 45.0f;
    int angle = 0;
    int cooldown = 1;
    float lastTime = 0;

    private Vector3 centerofmass;

    void Start()
    {

        rigidbody = transform.GetComponent<Rigidbody>();

        centerofmass = rigidbody.centerOfMass;
    }

    private void FixedUpdate()
    {

      
        rigidbody.centerOfMass = centerofmass;
    
        if (IsButtonMovingPressed) {


            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 0 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            Quaternion qua = this.transform.rotation;
            if (Physics.OverlapBox((this.transform.position + this.transform.forward * this.transform.localScale.z /2)  + ((this.transform.forward  ) * movementSpeed * Time.fixedDeltaTime), this.transform.localScale / 2, qua,0).Length> 0)
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                return;
            }


            this.rigidbody.MovePosition(this.transform.position + (this.transform.forward) * movementSpeed * Time.fixedDeltaTime);



        }


     
    }

    // Update is called once per frame
    void Update()
    {
        IsButtonMovingPressed = Input.GetKey(KeyCode.Space);



        var yangle = -1;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
          if(!invertupdowndir)
            yangle = 0;
          else
                yangle = 180;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!invertupdowndir)
                yangle = 180;
            else
                yangle = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            yangle = 270;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
               
            yangle = 90;
        }


    

        if (yangle > -1)
        {
            Quaternion target = Quaternion.Euler(0, yangle, 0);

            // Dampen towards the target rotation
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            transform.rotation = target;
        }

        if (false && Time.time > lastTime + cooldown)
        {


            angle = (angle + 90) % 360;

            Quaternion target = Quaternion.Euler(0, angle, 0);

            // Dampen towards the target rotation
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            transform.rotation = target;



            lastTime = Time.time;
        }

    }
}
