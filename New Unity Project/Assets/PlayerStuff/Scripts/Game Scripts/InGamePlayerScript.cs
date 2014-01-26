using UnityEngine;
using System.Collections;

public class InGamePlayerScript : MonoBehaviour 
{
	
	public GameObject camera;
	
	//HUD
	public static int BatteryCount = 0;
	public static float BatteryBar;
	public static float StaminaBar;
	public static float HealthBar;
	
	//Health
	public static float currentHealth = 100;
	public static float fullHeath = 100;
	
	//Damage by Monster
	private bool inRange;
	
	//Stamina
	public static float stamina = 100;
	public float stamRegen = 3.0f;
	public float stamDecay = 5.0f;
	public float sprintSpeed = 1.5f;
	private float stamDelay;
	private float timeDelay = 5.0f;
	private float energyCount;
	private bool sprint;
	private bool isMoving;
	public static bool StamEmpty = false;
	
	//Selecting Objects
	public RaycastHit hit;
	public int selectDistance = 10;
	
	GameObject Weapon;
	
	
	void Awake()
	{
		if (!networkView.isMine)
		{
			camera.SetActive(false);
			enabled = false;
		}
	
		StamEmpty = false;
	}
	
	// Use this for initialization
	void Start () 
	{
		BatteryBar = Screen.width/6;
		StaminaBar = Screen.width/6;
		HealthBar = Screen.width/6;
		
		Weapon = GameObject.FindGameObjectWithTag("Weapon");
	}
	
	// Update is called once per frame
	void Update () {
		
		TakingDamage();
		Running ();
		Selecting ();
	
	}
	
	void OnGUI()
	{	
		//Health
		GUI.Label (new Rect(Screen.width - 200, 10,80,30), "Health");
		GUI.Label (new Rect(Screen.width - 150, 10,80,30), currentHealth.ToString("F0"));
		GUI.Box (new Rect(Screen.width - 150, 10, HealthBar, 20), "");
		
		//Stamina
		GUI.Label (new Rect(10,10,80,30), "Stamina");
		GUI.Label (new Rect(70,10,80,20), stamina.ToString("F0"));
		if(!StamEmpty)
			GUI.Box (new Rect(70,10,StaminaBar,20), "");
		
		//Flashlight
		GUI.Label (new Rect(10,40,80,30), "Flashlight");
		GUI.Label (new Rect(70,40,80,20), Flashlight.BatteryLife.ToString("F0"));
		if(!Flashlight.EmptyLight)
			GUI.Box (new Rect(70,40,BatteryBar,20),"");
			//"F2" sets decimal precision to 2
		
		//Batteries
		GUI.Label(new Rect(10,70,80,50), "Extra Batteries");
		//converts BatteryCount from int to string
		GUI.Label(new Rect(70,70,80,30), BatteryCount.ToString());
	}
	
	void OnTriggerEnter()
	{
		inRange = true;
	}
	
	void OnTriggerExit()
	{
		inRange = false;
	}
	
	void TakingDamage()
	{
		if(Weapon)
		{
			if(inRange)
			{
				if(Monster.regAttk == true && currentHealth > 0)
				{
					currentHealth = currentHealth - (fullHeath/3); 
					HealthBar = (Screen.width/6) * (currentHealth * 0.01f);
				}
				else if(Monster.strongAttk == true && currentHealth > 0)
				{
					currentHealth = currentHealth - (2*fullHeath/3);
					HealthBar = (Screen.width/6) * (currentHealth * 0.01f);
					//pushing the player back?
					transform.Translate (Vector3.back * 10.0f);
					//affect stamina
				}
				else if(currentHealth <= 0)
				{
					currentHealth = 0;
				}
			}
		}
		
	}
	
	void Running()
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
				StaminaBar = (Screen.width/6) * (stamina * 0.01f);
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
					StaminaBar = (Screen.width/6) * (stamina * 0.01f);
					StamEmpty = false;
				}
			}
			else if(stamina == 0)
			{
				stamina = 0;
			}
		}
	}
	
	void Selecting()
	{
		//create ray in middle of camera
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
		
		if(Physics.Raycast(ray, out hit, selectDistance))
		{
			//make sure script is attached to object before funciton is called
			if(hit.collider.gameObject.GetComponent<Interact>() != null)
				hit.collider.gameObject.GetComponent<Interact>().OnLookEnter();
		}
	}

}
