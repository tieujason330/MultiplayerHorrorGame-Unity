using UnityEngine;
using System.Collections;

public class TextPopup : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		guiText.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		//text is empty string
		guiText.text = "";
	}
}
