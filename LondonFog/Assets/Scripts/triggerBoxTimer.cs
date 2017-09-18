using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBoxTimer : MonoBehaviour {

	public GameObject vig;
	triggerVignette vigScript;
	float timer = 0f;
	// Use this for initialization
	void Start () {
		vigScript = vig.GetComponent<triggerVignette> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//void OnTriggerEnter(Collider other)
	//{
		//if (other.gameObject.tag.Equals("Character")

	//}

	void OnTriggerStay(Collider other)
	{
		timer += Time.deltaTime;
		if (timer > 1.0f) {
			vigScript.isHurting = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		vigScript.isHurting = false;
	}

}
