using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowards : MonoBehaviour
{

	private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	RaycastHit hit;

    	Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
    	Debug.DrawRay(this.transform.position, fwd * 8, Color.green);
		
		if (Physics.Raycast(this.transform.position, fwd, out hit, 7))
	    {
	    	GameObject targetHit = hit.transform.gameObject;
			//do something if hit object ie
			if(targetHit.gameObject.tag == "character"){
				
				var characterTransformer = targetHit.GetComponent<Transform>();
				//characterTransformer.transform.LookAt(Camera.main.transform);

				var lookPos = this.transform.position - characterTransformer.transform.position;
				lookPos.y = 0;

				var rotation = Quaternion.LookRotation(lookPos);
				characterTransformer.transform.rotation = Quaternion.Slerp(characterTransformer.transform.rotation, rotation, Time.deltaTime * speed);
			}
		}
	}

}

