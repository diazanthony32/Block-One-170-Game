using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOuttaSight : MonoBehaviour
{

	Renderer rend; 

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
 	{
 		//this.GetComponent<Renderer>().enabled = false;
 		if (other.tag == "hideObject")
	    {
	 		//print("Entered");
	 		rend.enabled = false;

	    }
 	}

    void OnTriggerStay(Collider other)
 	{
 		//this.GetComponent<Renderer>().enabled = false;
 		if (other.tag == "hideObject")
	    {
	 		//print("Entered");
	 		rend.enabled = false;

	    }

 	}
 	void OnTriggerExit(Collider other)
 	{
 		//this.GetComponent<Renderer>().enabled = true;
 		if (other.tag == "hideObject")
	    {
	 		//print("Exitted");
	 		rend.enabled = true;

	    }
 	}

}
