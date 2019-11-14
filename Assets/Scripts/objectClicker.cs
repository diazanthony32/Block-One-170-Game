using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectClicker : MonoBehaviour
{

	// Use GetComponent to access the camera
    public Camera cam;

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
	//updates cube units tota;
	public TextMeshProUGUI pointsText;

	//used for determining units in play and max allowed to be in play
	public float numUnits = 0;
	public float maxUnits = 12;
	public float points = 0.0f;

	public float attackMode = 0.0f;

	//used to change the speed of the cube
	public float turnSpeed = 2.5f;

	public TurnManagement turn;

	public GameObject selectTop;
	public GameObject attackTop;

	public GameObject selectLeft;
	public GameObject attackLeft;

	public GameObject selectRight;
	public GameObject attackRight;

	//inner faces of cube
	public GameObject selectBottom;
	public GameObject attackBottom;

	public GameObject selectBackLeft;
	public GameObject attackBackLeft;

	public GameObject selectBackRight;
	public GameObject attackBackRight;

	GameObject whereToAttack;

	List<GameObject> hiddenFloatingPlanes = new List<GameObject>();
	List<GameObject> hiddenPlanes = new List<GameObject>();
	List<GameObject> hiddenFaces = new List<GameObject>();

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
	TextMeshProUGUI pointText;
    Transform directionsTransformer;

 
    // Start is called before the first frame update
    void Start()
    {

    	//Get the Transform component from the new cube
	   	blockTransformer = block.GetComponent<Transform>();
	   	text = unitText.GetComponent<TextMeshProUGUI>();
	   	pointText = pointsText.GetComponent<TextMeshProUGUI>();
        directionsTransformer = directions.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
    	//checks if the left mouse button is clicked and not currently moving, it uses the mouses position via raycast to see what has been clicked
    	if(Input.GetMouseButtonDown(0) && isCubeMoving == 0)
    	{
	        RaycastHit[] hits;
	        hits = Physics.RaycastAll(cam.ScreenPointToRay(Input.mousePosition), 100.0f);

	        	for (int i = 0; i < hits.Length; i++){
	        		RaycastHit hit = hits[i];
                    //print(hit.transform.gameObject.name);
            		CheckClick(hit.transform.gameObject);
	        	}
	    }

	        // RaycastHit[] highlight;
	        // highlight = Physics.RaycastAll(cam.ScreenPointToRay(Input.mousePosition), 100.0f);

	        // 	for (int i = 0; i < highlight.Length; i++){
	        // 		RaycastHit toBeHighlighted = highlight[i];
	        // 		if(toBeHighlighted.transform.gameObject.tag == "plane" && turn.gameState > 0){
	        // 			print("hovering over plane");

	        // 			var highlightTransformer = toBeHighlighted.transform.gameObject.GetComponent<Renderer>();

	        // 			highlightTransformer.sharedMaterial = material[1];
	        // 		}
	        // 	}
	    

	    //checks if XYZ has been set to do a turn, if so it makes it impossible to change directions until turn is finished
	   	if(tiltAroundX != 0 || tiltAroundY != 0 || tiltAroundZ != 0){

        	isCubeMoving = 1;

            // continously checks if the cube needs to move
            blockTransformer.transform.Rotate(new Vector3(tiltAroundX, tiltAroundY, tiltAroundZ) * Time.deltaTime * turnSpeed, Space.World);
    
        }
        else{
        	isCubeMoving = 0;
        }

        // continously checks if the cube needs to move
        //blockTransformer.transform.Rotate(new Vector3(tiltAroundX, tiltAroundY, tiltAroundZ) * Time.deltaTime * turnSpeed, Space.World);
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

    	pointText.text = "Points: " + points.ToString();
        //print(numUnits);

    }

    private void CheckClick(GameObject plane)
    {
    	if(turn.gameState == 1){
    		UnitHandler(plane);
	        RotateHandler(plane);
    	}
    	else if(points > 0){
    		RotateHandler(plane);
    	}

    	if(attackMode == 1 && points > 0){
    		attackHandler(plane);
    	}

    	//print(block.name + "points: " + points);
        
    }

    private void attackHandler (GameObject plane)
    {

        //Get the Renderer component from the plane
        var gamePlaneRenderer = plane.GetComponent<Renderer>();
        var gamePlaneTransformer = plane.GetComponent<Transform>();

        // if(plane.gameObject.tag == "planeNumber")
        // {
        //     print(plane.name);
        // }

        //checks if the plane selected is a "floating" target plane
        if(plane.gameObject.tag == "planeNumber")
        {
        	//print(plane.gameObject.name);
        	// attackTop.GetComponent<Transform>().childCount

        	if(gamePlaneTransformer.transform.parent.gameObject == selectTop)
        	{
        		whereToAttack = attackTop; 
        	}
        	else if(gamePlaneTransformer.transform.parent.gameObject == selectLeft)
        	{
        		whereToAttack = attackLeft; 
        	}
        	else if(gamePlaneTransformer.transform.parent.gameObject == selectRight)
        	{
        		whereToAttack = attackRight; 
        	}
        	else if(gamePlaneTransformer.transform.parent.gameObject == selectBottom)
        	{
        		whereToAttack = attackBottom; 
        	}
        	else if(gamePlaneTransformer.transform.parent.gameObject == selectBackLeft)
        	{
        		whereToAttack = attackBackLeft; 
        	}
        	else if(gamePlaneTransformer.transform.parent.gameObject == selectBackRight)
        	{
        		whereToAttack = attackBackRight; 
        	}

        	if(whereToAttack != null)
        	{

	        	for (int i = 0; i < whereToAttack.GetComponent<Transform>().childCount; i++)
	            {
	                //Destroy(gamePlaneTransformer.GetChild(i).gameObject);
	                //print(attackTop.name + ": ");
	                //print(attackTop.GetComponent<Transform>().GetChild(i).gameObject.name);

	                if(whereToAttack.GetComponent<Transform>().GetChild(i).gameObject.name == plane.gameObject.name){
	                	//print("Found a Match on Opposite Cube!");

	                	var attackPlane = whereToAttack.GetComponent<Transform>().GetChild(i).gameObject;
	                	var attackPlaneTransformer = attackPlane.GetComponent<Transform>();
	                	var attackPlaneRenderer = attackPlane.GetComponent<Renderer>();

	                	attackPlaneRenderer.sharedMaterial = material[1];

	                	RaycastHit hit;
				        if (Physics.Raycast(attackPlaneTransformer.transform.position, -attackPlaneTransformer.transform.up, out hit))
				        {
				            //print("Found an object: " + hit.transform.name);

				            //checks if the plane has already been selected, if so it resets it
						    //if(gamePlaneRenderer.sharedMaterial == material[1])
						    //{

				            var hitPlane = hit.transform.gameObject;
				            var hitPlaneTransformer = hitPlane.GetComponent<Transform>();
				            var hitPlaneRenderer = hitPlane.GetComponent<Renderer>();

				            if(hitPlaneTransformer.childCount > 0){
				            	for (int p = 0; p < hitPlaneTransformer.childCount; p++)
						        {
						        	// if(hitPlaneTransformer.childCount > 0){
						        		print("YOU HIT!");
						        		Destroy(hitPlaneTransformer.GetChild(p).gameObject);
						        		//turn.Player_2.numUnits -= 1;

						        		if(turn.Player_1 == this){
						        			//print("Clicked on Player_1");
						        			turn.Player_2.numUnits -= 1;
						        		}
						        		else if(turn.Player_2 == this){
						        			//print("Clicked on Player_2");
						        			turn.Player_1.numUnits -= 1;
						        		}

						            	hitPlaneRenderer.sharedMaterial = material[0];
						        	//}

						            // Destroy(hitPlaneTransformer.GetChild(p).gameObject);

						            // hitPlaneRenderer.sharedMaterial = material[0];
						        }
						    }
						    else{
						    	print("You Missed..");
						    }

							if(turn.Player_1 == this){
			        			//print("Clicked on Player_1");
			        			turn.Player_1.points -= 1;
			        		}
			        		else if(turn.Player_2 == this){
			        			//print("Clicked on Player_2");
			        			turn.Player_2.points -= 1;
			        		} 

				            //}
					        // for (int p = 0; p < hitPlaneTransformer.childCount; p++)
					        // {
					        //     Destroy(hitPlaneTransformer.GetChild(p).gameObject);

					        //     hitPlaneRenderer.sharedMaterial = material[0];
					        // }

					        //gamePlaneRenderer.sharedMaterial = material[0];

					        //numUnits -= 1;

						    //}

				        }

				        else{
				        	print("Raycast has failed to see anything...(Something is broken)");
				        }

	                	
	                }
	            }
        	}

        	else{
        		print("You clicked the wrong cube dummy");
        	}

            // //checks if the plane has already been selected, if so it resets it
            // if(gamePlaneRenderer.sharedMaterial == material[1])
            // {

            //     for (int i = 0; i < gamePlaneTransformer.childCount; i++)
            //     {
            //         Destroy(gamePlaneTransformer.GetChild(i).gameObject);
            //     }

            //     gamePlaneRenderer.sharedMaterial = material[0];

            //     numUnits -= 1;
            // }
            // // checks if we have reached the max amount of units allowed on the cube before we try to put anymore
            // else if(numUnits < maxUnits){

            //     GameObject character = Instantiate(characterCreate);
            //     var characterTransformer = character.GetComponent<Transform>();

            //     characterTransformer.transform.position = gamePlaneTransformer.transform.position;
            //     characterTransformer.transform.rotation = gamePlaneTransformer.transform.rotation;

            //     var rand = Random.Range(0, 8);
                        
            //     characterTransformer.transform.Translate(0.0f, 0.0f, 0.0f);
            //     characterTransformer.transform.Rotate(0.0f,(rand * 45.0f), 0.0f);

            //     characterTransformer.transform.SetParent(gamePlaneTransformer);

            //     //Call SetColor using the shader property name "_Color" and setting the color to red
            //     gamePlaneRenderer.sharedMaterial = material[1];

            //     numUnits += 1;

            // }
        }

    }

    private void UnitHandler (GameObject plane)
    {

        //Get the Renderer component from the plane
        var gamePlaneRenderer = plane.GetComponent<Renderer>();
        var gamePlaneTransformer = plane.GetComponent<Transform>();

        // if(plane.gameObject.tag == "planeNumber")
        // {
        //     print(plane.name);
        // }

        //checks if the plane selected is a child of the main cube
        if(plane.gameObject.tag == "plane" && gamePlaneTransformer.transform.parent.parent.parent == blockTransformer)
        {

            //checks if the plane has already been selected, if so it resets it
            if(gamePlaneRenderer.sharedMaterial == material[1])
            {

                for (int i = 0; i < gamePlaneTransformer.childCount; i++)
                {
                    Destroy(gamePlaneTransformer.GetChild(i).gameObject);
                }

                gamePlaneRenderer.sharedMaterial = material[0];

                numUnits -= 1;
            }
            // checks if we have reached the max amount of units allowed on the cube before we try to put anymore
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

    }

    private void RotateHandler (GameObject plane)
    {

        //checks whick arrow was clicked and gives a rotational speed equal to that direction

        //------- X

        if(plane.gameObject.name == directionsTransformer.GetChild(0).gameObject.name)
        {
            tiltAroundX = 90;
            checkGameState();
        }

        if(plane.gameObject.name == directionsTransformer.GetChild(1).gameObject.name)
        {
            tiltAroundX = -90;
            checkGameState();
        }
        
        //------- Y

        if(plane.gameObject.name == directionsTransformer.GetChild(2).gameObject.name)
        {
            tiltAroundZ = 90;
            checkGameState();
        }

        if(plane.gameObject.name == directionsTransformer.GetChild(3).gameObject.name)
        {
            tiltAroundZ = -90;
            checkGameState();
        }

        //--------- Z

        if(plane.gameObject.name == directionsTransformer.GetChild(4).gameObject.name)
        {
            tiltAroundY = 90;
            checkGameState();
        }

        if(plane.gameObject.name == directionsTransformer.GetChild(5).gameObject.name)
        {
            tiltAroundY = -90;
            checkGameState();
        }

    }

    private void checkGameState()
    {
    	if(turn.gameState != 1){
        	points -= 1;
        }
    }

    public void hideFrontP1()
    {

    	hide(turn.Player_1.selectTop);
    	hide(turn.Player_1.selectLeft);
    	hide(turn.Player_1.selectRight);

    }

    public void hideFrontP2()
    {

    	hide(turn.Player_2.selectTop);
    	hide(turn.Player_2.selectLeft);
    	hide(turn.Player_2.selectRight);

    }

    public void hide(GameObject selected)
    {

    	selected.SetActive(false);
    	hiddenFloatingPlanes.Add(selected);
    	// turn.Player_1.selectLeft.SetActive(false);
    	// turn.Player_1.selectRight.SetActive(false);

    	var topTransformer = selected.GetComponent<Transform>();
    	var midChildTransformer = topTransformer.GetChild(4).gameObject.GetComponent<Transform>();

        RaycastHit hit;
        if (Physics.Raycast(midChildTransformer.transform.position, -midChildTransformer.transform.up, out hit))
        {
            //print("Found an object 1: " + hit.transform.name);

            var parentPlane = hit.transform.parent.gameObject;
            hiddenPlanes.Add(parentPlane);
            parentPlane.SetActive(false);


            var transformer = parentPlane.GetComponent<Transform>();
	    	var midTransformer = transformer.GetChild(4).gameObject.GetComponent<Transform>();

	        RaycastHit hit2;
	        if (Physics.Raycast(midTransformer.transform.position, -midTransformer.transform.up, out hit2))
	        {
	            //print("Found an object 2: " + hit2.transform.name);

	            var parentPlane2 = hit2.transform.gameObject;
	            hiddenFaces.Add(parentPlane2);
	            parentPlane2.SetActive(false);
	        }

	    }

        else{
        	print("Raycast has failed to see anything...(Something is broken)");
        }

    }

    public void show()
    {
    	for(int i = 0; i < hiddenFloatingPlanes.Count; i++)
        {
        	//print(hiddenFloatingPlanes[i].name);
        	hiddenFloatingPlanes[i].SetActive(true);
        	//hiddenFloatingPlanes.RemoveAt(i);
        }

        //print(hiddenFloatingPlanes.Count);

        for(int i = 0; i < hiddenPlanes.Count; i++)
        {
        	//print(hiddenPlanes[i].name);
        	hiddenPlanes[i].SetActive(true);
        }

        for(int i = 0; i < hiddenFaces.Count; i++)
        {
        	//print(hiddenFaces[i].name);
        	hiddenFaces[i].SetActive(true);
        }

        hiddenFloatingPlanes.Clear();
        hiddenPlanes.Clear();
        hiddenFaces.Clear();
    }

}

