using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {
	
	public float range = 10;
	public float intensity = 1.5f;
	
	public float interval = 0.1f;
	private float lastTime = 0;
	
	private bool inRange;
	
	GameObject Person;
	
	void Start()
	{
		Person = GameObject.FindGameObjectWithTag("Monster");
	}
	// Update is called once per frame
	void OnTriggerEnter() 
	{
		inRange = true;
	}
	
	void OnTriggerExit()
	{
		inRange = false;
	}
	
	void Update()
	{
		Flickering ();
	}
	
	void Flickering()
	{
		if(Person)
		{
			if(inRange)
			{
				//if the last time it did something + interval is less than time that passed
				if (lastTime + interval <= Time.time)
				{
					lastTime = Time.time;
			
					gameObject.light.range = Random.Range (0, range);
					gameObject.light.intensity = Random.Range (0, intensity);
				}
			}
			else
			{
				gameObject.light.range = range;
				gameObject.light.intensity = intensity;
			}
		}
	}
}

		

