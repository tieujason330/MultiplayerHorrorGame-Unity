using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	
	//Energy
	public static float fullEnergy = 100.0f;
	public static float energy = 100.0f;
	public float energyRegen = 3.0f;
	public float energyDecay = 5.0f;
	public static bool idle = true;
	public static bool energyEmpty = false;
	public static float energyBar;
	private float energyDelay;
	private float energyCount;
	private float timeDelay = 5.0f;
	
	//Attack
	public static bool regAttk = false;
	public static bool strongAttk = false;
	
	//Jumping
	public static bool isJumping = false;
	
	public GameObject camera;
	
	void OnGUI()
	{
		GUI.Label (new Rect(10,10,80,30),"Energy");
		GUI.Label (new Rect(70,10,80,30), energy.ToString("F0"));
		if(!energyEmpty)
		{
			GUI.Box (new Rect(70,10,energyBar,20), "");
		}
	}
	
	void Awake()
	{
		if (!networkView.isMine)
		{
			camera.SetActive(false);
			enabled = false;
		}
		
		
		energyEmpty = false;
	}
	
	// Use this for initialization
	void Start () 
	{
		energyBar = Screen.width/6;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Attacking ();
		Energy ();
		Jumping ();
	}
	
	void Attacking()
	{
		if(Input.GetKeyDown (KeyCode.Mouse0))
		{
			idle = false;
			regAttk = true;
		}
		if(Input.GetKeyUp (KeyCode.Mouse0))
		{
			idle = true;
			regAttk = false;
			//energyDelay = Time.time;
		}
		if(Input.GetKeyDown(KeyCode.Mouse1) && energy == fullEnergy)
		{
			idle = false;
			strongAttk = true;
		}
		if(Input.GetKeyUp (KeyCode.Mouse1))
		{
			idle = true;
			strongAttk = false;
			//energyDelay = Time.time;
		}
	}
	
	void Energy()
	{
		if(idle == true)
		{
			if(energy < fullEnergy)
			{
				energyCount = Time.time - energyDelay;
				if(energyCount >= timeDelay)
				{
					energy = energy + (energyRegen * Time.deltaTime);
					energyBar = (Screen.width/6) * (energy * 0.01f);
					energyEmpty = false;
				}
			}
			else if(energy <= 0)
			{
				energy = 0;
				energyEmpty = true;
			}
		}
		else if(idle == false)
		{
			if(regAttk == true)
			{
				if(energy >= (fullEnergy * 0.666f))
				{
					energy = energy - (fullEnergy * 0.666f);
					energyBar = (Screen.width/6) * (energy * 0.01f);
					regAttk = false;
				}
			}
			else if(strongAttk == true)
			{
				if(energy >= fullEnergy)
				{
					energy = energy - fullEnergy;
					energyBar = (Screen.width/6) * (energy * 0.01f);
					strongAttk = false;
				}
			}
			else if(isJumping == true)
			{
				if(energy >= (fullEnergy * 0.25f))
				{
					energy = energy - (fullEnergy * 0.25f);
					energyBar = (Screen.width/6) * (energy * 0.01f);
					isJumping = false;
				}
			}
		}
	}
	
	void Jumping()
	{
		if(Input.GetKeyDown (KeyCode.Mouse2))
		{
			idle = false;
			isJumping = true;
		}
		if(Input.GetKeyUp (KeyCode.Mouse2))
		{
			idle = true;
			isJumping = false;
			//energyDelay = Time.time;
		}
	}
}
