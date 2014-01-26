using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour 
{
	
	public static float stamina = 100;
	public float stamRegen = 3.0f;
	public float stamDecay = 5.0f;
	
	public float sprintSpeed = 1.5f;
	
	private float stamDelay;
	private float timeDelay = 5.0f;
	public float energyCount;
	
	private bool sprint;
	private bool isMoving;
	
	public static bool StamEmpty = false;
	
	public enum Character
	{
		Player
	}
	
	public Character character;
	// Update is called once per frame
	
	void Awake()
	{
		StamEmpty = false;
	}
	void Update () 
	{
		if(character == Character.Player)
		{
			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				sprint = true;
			}
			if(Input.GetKeyUp(KeyCode.LeftShift))
			{
				sprint = false;
			}
			
			if(Input.GetKeyDown (KeyCode.W))
			{
				isMoving = true;
			}
			if(Input.GetKeyUp (KeyCode.W))
			{
				isMoving = false;
			}
			
			if(Input.GetKeyUp(KeyCode.LeftShift))
			{
				stamDelay = Time.time;
			}
			
			if(sprint && isMoving)
			{
				if(stamina > 0)
				{
					transform.Translate(Vector3.forward * sprintSpeed * Time.deltaTime);
					stamina = stamina - (stamDecay * Time.deltaTime);
					HUD.StaminaBar = (Screen.width/6) * (stamina * 0.01f);
					StamEmpty = false;
				}
				else if(stamina <= 0)
				{
					stamina = 0;
					sprint = false;
					StamEmpty = true;
				}
			}
			if(!sprint)
			{
				if(stamina < 100)
				{
					energyCount = Time.time - stamDelay;
					if(energyCount >= timeDelay)
					{
						stamina = stamina + (stamRegen * Time.deltaTime);
						HUD.StaminaBar = (Screen.width/6) * (stamina * 0.01f);
						StamEmpty = false;
					}
				}
				else if(stamina == 0)
				{
					stamina = 0;
				}
			}
		}
	}
}
