using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour 
{
	public RaycastHit hit;
	public int distance = 10;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		//create ray in middle of camera
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
		
		if(Physics.Raycast(ray, out hit, distance))
		{
			//make sure script is attached to object before funciton is called
			if(hit.collider.gameObject.GetComponent<Interact>() != null)
				hit.collider.gameObject.GetComponent<Interact>().OnLookEnter();
		}
	}
}
