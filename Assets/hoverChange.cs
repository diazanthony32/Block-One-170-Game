using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hoverChange : MonoBehaviour
{

	public Material hoverMaterial;
	private Material startMaterial;
	
	void OnMouseOver()
	{
		startMaterial = this.GetComponent<Renderer>().material;
		// highlightTransformer.sharedMaterial = material[1];

		GetComponent<Renderer>().material = hoverMaterial;
		print("entered");
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material = startMaterial;
		print("exited");
	}

}
