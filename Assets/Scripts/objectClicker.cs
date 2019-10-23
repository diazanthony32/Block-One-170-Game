using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectClicker : MonoBehaviour
{

	public GameObject block;
	public GameObject characterCreate;
	public Material[] material;

	//private public GameObject[] numberPlanes;

	float tiltAroundX = 0.0f;
	float tiltAroundY = 0.0f;
	float tiltAroundZ = 0.0f;

	float turnSpeed = 2.5f;

	float totalTurnX;
	float totalTurnY;
	float totalTurnZ;


    // Start is called before the first frame update
    void Start()
    {
        //numberPlanes = GameObject.FindGameObjectsWithTag("planeNumber");

    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetMouseButtonDown(0))
    	{
	        RaycastHit[] hits;
	        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f);

	        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	        //if(Physics.RaycastAll(ray, out hits, 100.0f))
	        //{
	        	for (int i = 0; i < hits.Length; i++){
	        		RaycastHit hit = hits[i];
            		//Renderer rend = hit.transform.GetComponent<Renderer>();
            		PrintName(hit.transform.gameObject);
	        	}
	        	// if(hit.transform != null)
	        	// {
	        	// 	PrintName(hit.transform.gameObject);
	        	// }
	        //}
	    }

	    //Get the Renderer component from the new cube
	   	var blockTransformer = block.GetComponent<Transform>();

	    //Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, tiltAroundZ);

     //    // Dampen towards the target rotation
        //blockTransformer.transform.rotation = Quaternion.Slerp(blockTransformer.transform.rotation, target,  Time.deltaTime * smooth);
        blockTransformer.transform.Rotate(new Vector3(tiltAroundX, tiltAroundY, tiltAroundZ) * Time.deltaTime * turnSpeed, Space.World);

        //print(new Vector3(tiltAroundX, tiltAroundY, tiltAroundZ) * Time.deltaTime * turnSpeed);

        totalTurnX += (tiltAroundX * Time.deltaTime * turnSpeed);
        //print("X: " + totalTurnX);

        if(Mathf.Abs(totalTurnX) > 90){
        	
        	print("Overshot X by: " + (Mathf.Abs(totalTurnX) - 90));
        	var Overshot = (Mathf.Abs(totalTurnX) - 90);

        	if(tiltAroundX < 0)
        	{
        		blockTransformer.transform.Rotate(new Vector3(Overshot, 0, 0), Space.World);
        	}
        	else
        	{
        		blockTransformer.transform.Rotate(new Vector3(-Overshot, 0, 0), Space.World);

        	}

        	tiltAroundX = 0;
        	totalTurnX = 0;
        }

        totalTurnY += (tiltAroundY * Time.deltaTime * turnSpeed);
        //print("Y: " + totalTurnY);

        if(Mathf.Abs(totalTurnY) > 90){

        	print("Overshot Y by: " + (Mathf.Abs(totalTurnY) - 90));
        	var Overshot = (Mathf.Abs(totalTurnY) - 90);

        	if(tiltAroundY < 0)
        	{
        		blockTransformer.transform.Rotate(new Vector3(0, Overshot, 0), Space.World);
        	}
        	else
        	{
        		blockTransformer.transform.Rotate(new Vector3(0, -Overshot, 0), Space.World);

        	}

        	//blockTransformer.transform.Rotate(new Vector3(0, -Overshot, 0), Space.World);

        	tiltAroundY = 0;
        	totalTurnY = 0;
        }

        totalTurnZ += (tiltAroundZ * Time.deltaTime * turnSpeed);
        //print("Z: " + totalTurnZ);

        if(Mathf.Abs(totalTurnZ) > 90){

        	print("Overshot Z by: " + (Mathf.Abs(totalTurnZ) - 90));
        	var Overshot = (Mathf.Abs(totalTurnZ) - 90);

        	if(tiltAroundZ < 0)
        	{
        		blockTransformer.transform.Rotate(new Vector3(0, 0, Overshot), Space.World);
        	}
        	else
        	{
        		blockTransformer.transform.Rotate(new Vector3(0, 0, -Overshot), Space.World);

        	}

        	//blockTransformer.transform.Rotate(new Vector3(0, 0, -Overshot), Space.World);

        	tiltAroundZ = 0;
        	totalTurnZ = 0;
        }

        // if(blockTransformer.transform.rotation.x)
        // {
        // 	tiltAroundX = 0;
        // }

        // if(blockTransformer.transform.rotation.x == tiltAroundX){
        // 	print("HI");
        // }

        //print(Mathf.Abs(blockTransformer.transform.rotation.x * 180));

        //print("(" + tiltAroundX + ", " + tiltAroundY + ", " + tiltAroundZ + ")");

    }

    private void PrintName(GameObject plane)
    {
    	if(plane.gameObject.tag == "planeNumber")
    	{
    		print(plane.name);
	    }

	    if(plane.gameObject.tag == "plane")
    	{
	        //Get the Renderer component from the new cube
	       	var gamePlaneRenderer = plane.GetComponent<Renderer>();
	       	var gamePlaneTransformer = plane.GetComponent<Transform>();

			if(gamePlaneRenderer.material.GetColor("_Color") == Color.red)
			{
				//Call SetColor using the shader property name "_Color" and setting the color to red
				gamePlaneRenderer.sharedMaterial = material[0];
			}
			else{

				GameObject nathan = Instantiate(characterCreate);
		       	var nathanTransformer = nathan.GetComponent<Transform>();

		       	nathanTransformer.transform.position = gamePlaneTransformer.transform.position;
		       	nathanTransformer.transform.rotation = gamePlaneTransformer.transform.rotation;
		       	nathanTransformer.transform.Translate(0.0f,0.1f,0.0f);

		       	nathanTransformer.transform.SetParent(gamePlaneTransformer);

				//Call SetColor using the shader property name "_Color" and setting the color to red
				gamePlaneRenderer.sharedMaterial = material[1];

			}
	    }

	    if(plane.gameObject.name == "NathanPlane(Clone)")
    	{
	        Destroy(plane);
	    }

	    //------ DONE

	    if(plane.gameObject.name == "UpLeft")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			//Call SetColor using the shader property name "_Color" and setting the color to red
			// blockTransformer.transform.Rotate(90,0,0,Space.World);
			tiltAroundX = 90;
	    }

	    if(plane.gameObject.name == "DownLeft")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			//Call SetColor using the shader property name "_Color" and setting the color to red
			// blockTransformer.transform.Rotate(-90,0,0,Space.World);

			tiltAroundX = -90;
	    }
	    
	    //------- DONE

	    if(plane.gameObject.name == "UpRight")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			// //Call SetColor using the shader property name "_Color" and setting the color to red
			// blockTransformer.transform.Rotate(0,0,90,Space.World);

			tiltAroundZ = 90;
	    }

	    if(plane.gameObject.name == "DownRight")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			// //Call SetColor using the shader property name "_Color" and setting the color to red
			// blockTransformer.transform.Rotate(0,0,-90,Space.World);

			tiltAroundZ = -90;
	    }

	    //--------- DONE

	    if(plane.gameObject.name == "LeftTurn")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			// //Call SetColor using the shader property name "_Color" and setting the color to red
			// blockTransformer.transform.Rotate(0,90,0,Space.World);
			tiltAroundY = 90;
	    }

	    if(plane.gameObject.name == "RightTurn")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			//Call SetColor using the shader property name "_Color" and setting the color to red
			// blockTransformer.transform.Rotate(0,-90,0,Space.World);

	       	tiltAroundY = -90;

			// Quaternion target = Quaternion.Euler(0, -90, 0);

   		//      	// Dampen towards the target rotation
   		//      	blockTransformer.transform.rotation = Quaternion.Slerp(blockTransformer.transform.rotation, target,  Time.deltaTime * 6.0f);
	    }


    }



}
