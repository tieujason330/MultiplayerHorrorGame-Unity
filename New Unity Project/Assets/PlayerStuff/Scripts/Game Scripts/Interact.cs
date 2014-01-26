using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {
	public GUIText target;
	private bool selected = false;
	
	public enum Item
	{
		Battery,
		Player
	}
	
	public Item item;
	
	// Update is called once per frame
	void Update () 
	{
		renderer.material.color = Color.clear;
		selected = false;
	}
	
	public void OnLookEnter()
	{
		//when mousing over an object, changes color of object to red
		renderer.material.color = Color.red;
		target.text = "Press E to interact";
		selected = true;
		
		if(Input.GetKeyDown(KeyCode.E) && selected)
		{ 
			if(item == Item.Battery)
			{
				InGamePlayerScript.BatteryCount++;
				Destroy(gameObject);
			}
		}
	}
	
	void OnGUI()
	{
		/*if(Input.GetKeyDown(KeyCode.E) && selected)
		{ 
			if(item == Item.Battery)
			{
				HUD.BatteryCount++;
				Destroy(gameObject);
			}
		}
		
		Event e = Event.current;
		if (e.isKey && e.character == 'e' && selected)
		{
			if(item == Item.Battery)
			{
				HUD.BatteryCount++;
				Destroy(gameObject);
			}
			
		}*/
	}
}
