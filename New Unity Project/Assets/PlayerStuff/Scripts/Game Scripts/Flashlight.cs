using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour 
{
	public static float BatteryLife = 100;
	
	public Light HeadLight;
	public float BatteryReductionSpeed = 3.0f;
	public float BatteryFlickerStart = 1;
	public float Flashlightrange = 22.15f;
	public float Flashlightintensity = 4.0f;
	public float flashinterval = 0.0f;
	public float lastTime = 0;
	
	public static bool EmptyLight = false;

	void Awake()
	{
		HeadLight.enabled = false;
	}
	
	void Update()
	{
		usingFlashlight ();
	}
	
	void usingFlashlight()
	{
		if(HeadLight.enabled)
		{
			BatteryLife = BatteryLife - (BatteryReductionSpeed * Time.deltaTime);
			InGamePlayerScript.BatteryBar = (Screen.width/6) * (BatteryLife * 0.01f);
			if(BatteryLife <= BatteryFlickerStart)
			{
				if (lastTime + flashinterval <= Time.time)
				{
					lastTime = Time.time;
					
					HeadLight.range = Random.Range (0, Flashlightrange);
					HeadLight.intensity = Random.Range (0, Flashlightintensity);
				}
			}
			//deltaTime is the measurement of time between frames, reduces in "realtime"
		}
		
		if(Input.GetKeyDown (KeyCode.Mouse0) && !HeadLight.enabled)
		{
			if(BatteryLife <= 0 && InGamePlayerScript.BatteryCount > 0)
			{
				InGamePlayerScript.BatteryCount--;
				BatteryLife = 100;
				HeadLight.range = Flashlightrange;
				HeadLight.intensity = Flashlightintensity;
			}
			HeadLight.enabled = true;
			EmptyLight = false;
		}
		else if(Input.GetKeyDown(KeyCode.Mouse0) && HeadLight.enabled)
		{
			HeadLight.enabled = false;
		}
		
		if(BatteryLife <= 0)
		{
			BatteryLife = 0;
			HeadLight.enabled = false;
			EmptyLight = true;
		}
	}
}
