using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    public Rigidbody rb;

	public bool interactKeyPressed = false;
	public bool touchCube = false;
	//public bool pushing = false;
	private GameObject inContact;

    // Use this for initialization
    void Start () {
        speed = 5.5f;
        sensitivity = 10f;
        rb = GetComponent<Rigidbody>();
       
    }
	
	// Update is called once per frame
	void Update () {
        

        //smooth jumping animation
        if (Input.GetKeyDown(KeyCode.Space))// && rb.velocity.y<=0)
        {
            if (rb.velocity.y <=.01)
            {
                rb.AddForce(transform.up * (speed-rb.position.y), ForceMode.Impulse);
            }
        }


		if (Input.GetKey (KeyCode.E)) 
		{
			interactKeyPressed = true;
		} else 
		{
			interactKeyPressed = false;
		}
			
        //Moves Forward and back along z axis                           //Up/Down
        transform.Translate(-Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);

        //Moves Left and right along x Axis                               //Left/Right
        transform.Translate(-Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);


		if (touchCube && interactKeyPressed) 
		{
			Vector3 res = (-Vector3.right * Time.deltaTime * Input.GetAxis ("Horizontal") * speed);
			Vector3 res2 = (-Vector3.forward * Time.deltaTime * Input.GetAxis ("Vertical") * speed);
			Vector3 otherPos = inContact.GetComponent<Rigidbody> ().position;

			Vector3 res3 = res + res2;
			res3 = transform.TransformDirection(res3);
			inContact.GetComponent<Rigidbody>().MovePosition(otherPos + res3);
		}

        
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
        
    }


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "cube") 
		{
			touchCube = true;
			inContact = other.gameObject;
		}

	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "cube") 
		{
			touchCube = false;
			inContact = null;
		}

	}


}
