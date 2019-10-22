using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectClicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetMouseButtonDown(0))
    	{
	        RaycastHit hit;
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	        if(Physics.Raycast(ray, out hit, 100.0f))
	        {
	        	if(hit.transform != null)
	        	{
	        		PrintName(hit.transform.gameObject);
	        	}
	        }
	    }
    }

    private void PrintName(GameObject plane)
    {
    	if(plane.gameObject.tag == "plane")
    	{
    		print(plane.name);

	    	//Get the Renderer component from the new cube
	       	var planeRenderer = plane.GetComponent<Renderer>();

	       	//Call SetColor using the shader property name "_Color" and setting the color to red
	       	planeRenderer.material.SetColor("_Color", Color.red);
       }
       else{
       	print("Plane not Selected");
       }
    }

}
