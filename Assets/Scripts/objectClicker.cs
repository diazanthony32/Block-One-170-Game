using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectClicker : MonoBehaviour
{

	//determines what block to modify
	public GameObject block;

	//placeholder for character spawning
	public GameObject characterCreate;

	//used for changing the color of the faces of the cube
	public Material[] material;

	//used to see what directions are being used for which cube
	public GameObject directions;

	//updates cube units tota;
	public TextMeshProUGUI unitText;

	//used for determining units in play and max allowed to be in play
	public float numUnits = 0;
	public float maxUnits = 12;

	//used to change the speed of the cube
	public float turnSpeed = 2.5f;

	//simple check if the cube is moving
	float isCubeMoving = 0;

	//used in the turning of the cube, if XYZ isnt 0 it moves
	float tiltAroundX = 0.0f;
	float tiltAroundY = 0.0f;
	float tiltAroundZ = 0.0f;

	//used to keep track of where the position of the cube is in space
	float totalTurnX;
	float totalTurnY;
	float totalTurnZ;


	Transform blockTransformer;
	TextMeshProUGUI text;
    Transform directionsTransformer;

 
    // Start is called before the first frame update
    void Start()
    {

    	//Get the Transform component from the new cube
	   	blockTransformer = block.GetComponent<Transform>();
	   	text = unitText.GetComponent<TextMeshProUGUI>();
        directionsTransformer = directions.GetComponent<Transform>();
    
    }

    // Update is called once per frame
    void Update()
    {
    	//checks if the left mouse button is clicked and not currently moving, it uses the mouses position via raycast to see what has been clicked
    	if(Input.GetMouseButtonDown(0) && isCubeMoving == 0)
    	{
	        RaycastHit[] hits;
	        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f);

	        	for (int i = 0; i < hits.Length; i++){
	        		RaycastHit hit = hits[i];
            		CheckClick(hit.transform.gameObject);
	        	}
	    }

	    //checks if XYZ has been set to do a turn, if so it makes it impossible to change directions until turn is finished
	   	if(tiltAroundX != 0 || tiltAroundY != 0 || tiltAroundZ != 0){
        	isCubeMoving = 1;
        }
        else{
        	isCubeMoving = 0;
        }

        // continously checks if the cube needs to move
        blockTransformer.transform.Rotate(new Vector3(tiltAroundX, tiltAroundY, tiltAroundZ) * Time.deltaTime * turnSpeed, Space.World);
        	//print(new Vector3(tiltAroundX, tiltAroundY, tiltAroundZ) * Time.deltaTime * turnSpeed);

        //checks if the X axis reaches a 90 degree turn, if so it resets variables -------------------------------
        totalTurnX += (tiltAroundX * Time.deltaTime * turnSpeed);
        	//print("X: " + totalTurnX);
        if(Mathf.Abs(totalTurnX) > 90){
        	
        	//print("Overshot X by: " + (Mathf.Abs(totalTurnX) - 90));
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

        //checks if the Y axis reaches a 90 degree turn, if so it resets variables -------------------------------
        totalTurnY += (tiltAroundY * Time.deltaTime * turnSpeed);
        	//print("Y: " + totalTurnY);
        if(Mathf.Abs(totalTurnY) > 90){

        	//print("Overshot Y by: " + (Mathf.Abs(totalTurnY) - 90));
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

        //checks if the Z axis reaches a 90 degree turn, if so it resets variables --------------------------------
        totalTurnZ += (tiltAroundZ * Time.deltaTime * turnSpeed);
        	//print("Z: " + totalTurnZ);
        if(Mathf.Abs(totalTurnZ) > 90){

        	//print("Overshot Z by: " + (Mathf.Abs(totalTurnZ) - 90));
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

        if(numUnits == maxUnits){
        	text.text = "Units on Cube: (MAX)";
        }
        else{
        	text.text = "Units on Cube: " + numUnits.ToString();
    	}
        //print(numUnits);

    }

    private void CheckClick(GameObject plane)
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

				for (int i = 0; i < gamePlaneTransformer.childCount; i++)
				{
				    Destroy(gamePlaneTransformer.GetChild(i).gameObject);
				}

				gamePlaneRenderer.sharedMaterial = material[0];

				numUnits -= 1;
			}
			else if(numUnits < maxUnits){

				GameObject character = Instantiate(characterCreate);
		       	var characterTransformer = character.GetComponent<Transform>();

		       	characterTransformer.transform.position = gamePlaneTransformer.transform.position;
		       	characterTransformer.transform.rotation = gamePlaneTransformer.transform.rotation;

		       	var rand = Random.Range(0, 8);
		       	
		       	characterTransformer.transform.Translate(0.0f, 0.0f, 0.0f);
		       	characterTransformer.transform.Rotate(0.0f,(rand * 45.0f), 0.0f);

		       	characterTransformer.transform.SetParent(gamePlaneTransformer);

				//Call SetColor using the shader property name "_Color" and setting the color to red
				gamePlaneRenderer.sharedMaterial = material[1];

				numUnits += 1;

			}
	    }

	  //   //------ DONE

	  //   if(plane.gameObject.name == "UpLeft")
   //  	{
			// tiltAroundX = 90;
	  //   }

	  //   if(plane.gameObject.name == "DownLeft")
   //  	{
			// tiltAroundX = -90;
	  //   }
	    
	  //   //------- DONE

	  //   if(plane.gameObject.name == "UpRight")
   //  	{
			// tiltAroundZ = 90;
	  //   }

	  //   if(plane.gameObject.name == "DownRight")
   //  	{
			// tiltAroundZ = -90;
	  //   }

	  //   //--------- DONE

	  //   if(plane.gameObject.name == "LeftTurn")
   //  	{
			// tiltAroundY = 90;
	  //   }

	  //   if(plane.gameObject.name == "RightTurn")
   //  	{
	  //      	tiltAroundY = -90;
	  //   }

	    //print(directions.GetChild(0).gameObject.name);

        // for (int i = 0; i < directionsTransformer.childCount; i++)
        // {
        //     print(directionsTransformer.GetChild(i).gameObject.name);

            if(plane.gameObject.name == directionsTransformer.GetChild(0).gameObject.name)
            {
                tiltAroundX = 90;
            }

            if(plane.gameObject.name == directionsTransformer.GetChild(1).gameObject.name)
            {
                tiltAroundX = -90;
            }
            
            //------- DONE

            if(plane.gameObject.name == directionsTransformer.GetChild(2).gameObject.name)
            {
                tiltAroundZ = 90;
            }

            if(plane.gameObject.name == directionsTransformer.GetChild(3).gameObject.name)
            {
                tiltAroundZ = -90;
            }

            //--------- DONE

            if(plane.gameObject.name == directionsTransformer.GetChild(4).gameObject.name)
            {
                tiltAroundY = 90;
            }

            if(plane.gameObject.name == directionsTransformer.GetChild(5).gameObject.name)
            {
                tiltAroundY = -90;
            }
        //}

    }

}
