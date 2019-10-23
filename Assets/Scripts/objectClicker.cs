using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectClicker : MonoBehaviour
{

	public GameObject block;

	// float tiltAroundX = 0.0f;
	// float tiltAroundY = 0.0f;
	// float tiltAroundZ = 0.0f;

	// float smooth = 6.0f;


    // Start is called before the first frame update
    void Start()
    {
        
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
	   	// var blockTransformer = block.GetComponent<Transform>();

	    // Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, tiltAroundZ);

     //    // Dampen towards the target rotation
     //    blockTransformer.transform.rotation = Quaternion.Slerp(blockTransformer.transform.rotation, target,  Time.deltaTime * smooth);

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

			//Call SetColor using the shader property name "_Color" and setting the color to red
			gamePlaneRenderer.material.SetColor("_Color", Color.red);
	    }

	    //------ DONE

	    if(plane.gameObject.name == "UpLeft")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			//Call SetColor using the shader property name "_Color" and setting the color to red
			blockTransformer.transform.Rotate(90,0,0,Space.World);
			// tiltAroundX += 90;
	    }

	    if(plane.gameObject.name == "DownLeft")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			//Call SetColor using the shader property name "_Color" and setting the color to red
			blockTransformer.transform.Rotate(-90,0,0,Space.World);

			// tiltAroundX += -90;
	    }
	    
	    //------- DONE

	    if(plane.gameObject.name == "UpRight")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			// //Call SetColor using the shader property name "_Color" and setting the color to red
			blockTransformer.transform.Rotate(0,0,90,Space.World);

			// tiltAroundZ += 90;
	    }

	    if(plane.gameObject.name == "DownRight")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			// //Call SetColor using the shader property name "_Color" and setting the color to red
			blockTransformer.transform.Rotate(0,0,-90,Space.World);

			// tiltAroundZ += -90;
	    }

	    //--------- DONE

	    if(plane.gameObject.name == "LeftTurn")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			// //Call SetColor using the shader property name "_Color" and setting the color to red
			blockTransformer.transform.Rotate(0,90,0,Space.World);
			// tiltAroundY += 90;
	    }

	    if(plane.gameObject.name == "RightTurn")
    	{
	        //Get the Renderer component from the new cube
	       	var blockTransformer = block.GetComponent<Transform>();

			//Call SetColor using the shader property name "_Color" and setting the color to red
			blockTransformer.transform.Rotate(0,-90,0,Space.World);

	       	// tiltAroundY += -90;

			// Quaternion target = Quaternion.Euler(0, -90, 0);

   //      	// Dampen towards the target rotation
   //      	blockTransformer.transform.rotation = Quaternion.Slerp(blockTransformer.transform.rotation, target,  Time.deltaTime * 6.0f);
	    }


    }

}
