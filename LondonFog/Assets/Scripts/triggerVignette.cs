using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class triggerVignette : MonoBehaviour
{


	public bool isHurting = false;

	float countSecond = 0.0f;
	Color col;
	Image blackScreen;
	// Use this for initialization
	void Start ()
	{
		Image[] arr = gameObject.GetComponentsInChildren<Image> ();
		blackScreen = arr [1];
		col = blackScreen.color;
		//float asdf = blackScreen.color[3];
		col.a = 0.0f;
		blackScreen.color = col;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (isHurting && countSecond >= 1.0f) {
//			col[3] += 0.1f;
//			blackScreen.color = col;
//			countSecond = 0.0f;
//		} else if (blackScreen.color [3] > 0.0 && countSecond >= 1.0f) {
//			col[3] -= 0.1f;
//			blackScreen.color = col;
//			countSecond = 0.0f;
//		}
//		countSecond += Time.deltaTime;
		if (isHurting) {
			col.a += (0.1f * Time.deltaTime);
			blackScreen.color = col;
		} else if (blackScreen.color.a > 0.0f) {
			col.a -= (0.2f * Time.deltaTime);
			blackScreen.color = col;
		}
			

	}
		

}
