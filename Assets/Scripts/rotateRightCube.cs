using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transform.rotation example.

// Rotate a GameObject using a Quaternion.
// Tilt the cube using the arrow keys. When the arrow keys are released
// the cube will be rotated back to the center using Slerp.


public class rotateRightCube : MonoBehaviour
{

	float smooth = 6.0f;
    float tiltAngle = 90.0f;
    float tiltSpeed = 2.0f;

    float tiltAroundZ = 0.0f;
    float tiltAroundX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    	if (Input.GetKey(KeyCode.UpArrow))
        {
            //print("vertical key is held down");
            if(tiltAroundX <= tiltAngle)
            {
            	tiltAroundX += tiltSpeed;
            }
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //print("vertical key is held down");
            if(tiltAroundX >= -tiltAngle)
            {
            	tiltAroundX -= tiltSpeed;
            }
        }

        else
        {
        	//resets the cube back to starting position
        	if(tiltAroundX > 0.0f)
            {
            	tiltAroundX -= tiltSpeed;
            }
            if(tiltAroundX < 0.0f)
            {
            	tiltAroundX += tiltSpeed;
            }
        }

        //---------------------------------------------------------------------------

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //print("vertical key is held down");
            if(tiltAroundZ <= tiltAngle)
            {
            	tiltAroundZ += tiltSpeed;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //print("vertical key is held down");
            if(tiltAroundZ >= -tiltAngle)
            {
            	tiltAroundZ -= tiltSpeed;
            }
        }

        else
        {
        	//resets the cube back to starting position
        	if(tiltAroundZ > 0.0f)
            {
            	tiltAroundZ -= tiltSpeed;
            }
            if(tiltAroundZ < 0.0f)
            {
            	tiltAroundZ += tiltSpeed;
            }
        }

		//---------------------------------------------------------------------------------------

        // Smoothly tilts a transform towards a target rotation.
        //float tiltAroundZ = -Input.GetAxis("Horizontal") * tiltAngle;
        //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        // This is also our resting position of the cube
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
}
