using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed = 2f;
    // Start is called before the first frame update

    float smooth = 5.0f;
    float tiltAngle = 45.0f;
    int angle = 0;
    int cooldown = 1;
    float lastTime = 0;

    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastTime + cooldown)
        {

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //rigidbody.transform.localRotation

        //if (Input.GetKeyDown(KeyCode.DownArrow))

        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        angle = (angle+90)% 360;

        Quaternion target = Quaternion.Euler(0, angle, 0);

        // Dampen towards the target rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        transform.rotation = target;

        

        lastTime = Time.time;
        }

    }
}
