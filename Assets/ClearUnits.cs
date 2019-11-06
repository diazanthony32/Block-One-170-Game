using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearUnits : MonoBehaviour
{

	public Button button;
	public Material[] material;

	GameObject[] planes;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick(){

		//Debug.Log ("You have clicked the button!");

		if (planes == null)
		{
            planes = GameObject.FindGameObjectsWithTag("plane");
		}

		foreach (GameObject plane in planes)
        {

	       	var planeRenderer = plane.GetComponent<Renderer>();
        	var planeTransformer = plane.GetComponent<Transform>();

            for (int i = 0; i < planeTransformer.childCount; i++)
				{
				    Destroy(planeTransformer.GetChild(i).gameObject);
				}

			planeRenderer.sharedMaterial = material[0];

			objectClicker[] cameraScripts = Camera.main.GetComponents<objectClicker>();
        	
        	var scriptOne = cameraScripts[0];
        	var scriptTwo = cameraScripts[1];

        	scriptOne.numUnits = 0.0f;
        	scriptTwo.numUnits = 0.0f;
        }

	}

}
