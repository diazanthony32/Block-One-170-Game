﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateView : MonoBehaviour
{

	// private float tiltAroundX = 0.0f;
	// private float tiltAroundY = 0.0f;
	// private float tiltAroundZ = 0.0f;

	private float speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	// Vector3 targetPosition = new Vector3(0, Camera.main.transform.position.y, 0);
        // this.transform.LookAt(Camera.main.transform);
        // Rigidbody m_Rigidbody = this.GetComponent<Rigidbody>();
        // m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;

        //this.transform.Rotate(0,1,0);
        // this.transform.rotation = Quaternion.Euler(-5, 135, 0);

        // Vector3 direction = Camera.main.transform.position - this.transform.position;
        // print(direction);
        // Quaternion rotation = Quaternion.LookRotation(direction);

        // this.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

	    var lookPos = Camera.main.transform.position - this.transform.position;
		lookPos.y = 0;

		print(this.transform.position);
        Vector3 cubePosition = new Vector3(0, 0, 0);

        if(this.transform.position.y > 6.1){
        	cubePosition = new Vector3(0, 1, 0);
        }
        else if(this.transform.position.x < 5.1){
        	cubePosition = new Vector3(-1, 0, 0);
        	lookPos.z += 3;
        }
        else if(this.transform.position.x > 5.1){
        	cubePosition = new Vector3(1, 0, 0);
        	lookPos.z -= 1000;
        }

		
        var rotation = Quaternion.LookRotation(lookPos,cubePosition);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * speed);

		// Vector3 targetPostition = new Vector3( this.transform.position.x, this.transform.position.y, this.transform.position.z ) ;
 	// 	this.transform.LookAt( targetPostition ) ;

    	//Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    	//print(this.transform.rotation.y * Time.deltaTime * speed);

    	//var totalTurnX += (tiltAroundX * Time.deltaTime * turnSpeed);

    	// var rotation = Quaternion.LookRotation(targetPosition);
    	// //print(rotation);
    	// this.transform.Rotate(new Vector3(0, tiltAroundY, 0) * Time.deltaTime * speed, Space.Self);

    }
}
