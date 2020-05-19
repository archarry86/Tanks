using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    Rigidbody rigidbody;
    public float movementSpeed = 20f;

    public bool invertVerticalMovement = false;
    public bool invertHorizontal = false;


    bool IsButtonMovingPressed = false;

    float tiltAngle = 45.0f;
    int angle = 0;
    int cooldown = 1;
    float lastTime = 0;

    private Vector3 centerofmass;

    void Start()
    {

        rigidbody = transform.GetComponent<Rigidbody>();

        centerofmass = this.transform.position;
    }

    private void FixedUpdate()
    {

      
      //rigidbody.centerOfMass = centerofmass;
    
        if (IsButtonMovingPressed) {


            
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 0 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = LayerMask.GetMask("Tanks"); 
           
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            float  fdistance =   movementSpeed * Time.fixedDeltaTime *2;
            if (
            Physics.BoxCast(this.transform.position, this.transform.localScale , this.transform.forward, out hit, this.transform.rotation, fdistance, layerMask) 
           
            ){
               // //Debug.Log(Time.time + " Box Cast "+this.gameObject.name);
               //
               // //Debug.Log(Time.time+" "+(hit.transform.gameObject.name));
               // //Debug.Log(Time.time+" "+(hit.distance.ToString()));
               // //Debug.Log(Time.time+" "+(hit.point.ToString()));
               // //Debug.Log(Time.time+" "+(this.transform.position - hit.transform.position));

                if (hit.distance < 1) {
                    // //Debug.Log(Time.time + " NO MOVEMENT");
                   return;
                }
               
               
            }

        
            this.rigidbody.MovePosition(this.transform.position + (this.transform.forward * fdistance));
            

      
        }

        // nosirvio
        //   this.rigidbody.AddForce(this.transform.forward * movementSpeed * Time.fixedDeltaTime * 10);





    }

    // Update is called once per frame
    void Update()
    {
        IsButtonMovingPressed = false;



        var yangle = -1;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
          if (!invertVerticalMovement)
            yangle = 0;
          else
                yangle = 180;
        }

        

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
         
            if (!invertVerticalMovement)
                yangle = 180;
            else
                yangle = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
            if (!invertHorizontal)
            yangle = 270;
            else
                yangle = 90;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
          
            if (!invertHorizontal)
                yangle = 90;
            else
                yangle = 270;

        }

      
        IsButtonMovingPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);



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
        /*
        if (IsButtonMovingPressed) {
            this.rigidbody.AddForce(this.transform.forward * movementSpeed * Time.deltaTime );

        }*/

    }
}
