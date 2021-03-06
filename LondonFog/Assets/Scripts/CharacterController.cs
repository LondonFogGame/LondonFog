﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    public Rigidbody rb;

	public bool interactKeyPressed = false;
	public bool touchCube = false;

    public bool groundContact = false;

    public bool touchSphere = false;
    public bool hasKey = false;

    public bool atGate = false;

	public bool isJumping = false;

    public int newspaperCount;
    public int deliveryCount;
    public List<Point> deliverLocations;
    public GameObject arrow;
    public float dampling;
    private int deliveryIndex = 0;

    public int health;
    public bool inFog = false;
    
    //public bool pushing = false;
    private GameObject inContact;

	public Animator m_animator;

    // Use this for initialization
    void Start () {
        health = 5;
        speed = 4.5f;
        sensitivity = 10f;
        rb = GetComponent<Rigidbody>();    
    }
	
	// Update is called once per frame
	void Update ()
    {

		m_animator.SetBool("Grounded", groundContact);
        

        //smooth jumping animation
        //if ((Input.GetKeyDown(KeyCode.Space) && groundContact) ||(Input.GetKeyDown(KeyCode.Space) && touchCube))// && rb.velocity.y<=0)

		if (Input.GetKeyDown(KeyCode.Space) && groundContact && !isJumping)// && rb.velocity.y<=0)

        {
            
			isJumping = true;
            rb.AddForce(transform.up * (speed), ForceMode.Impulse);
            
        }


        if(Input.GetKey(KeyCode.E) && hasKey && atGate)
        {
            Vector3 res = new Vector3(15.52327f, 3.342756f, -0.7637978f);
            inContact.GetComponent<Rigidbody>().MovePosition(res);
        }

		if (Input.GetKey (KeyCode.E)) 
		{
            if(touchSphere == true)
            {
                hasKey = true;
            }
			interactKeyPressed = true;
		}
        else 
		{
			interactKeyPressed = false;
		}
			
        //Moves Forward and back along z axis                           //Up/Down
        //transform.Translate(-Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);
		transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);




        //Moves Left and right along x Axis                               //Left/Right
        //transform.Translate(-Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);
		transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);

			m_animator.SetFloat ("MoveSpeed", (Input.GetAxis ("Vertical") * speed));



        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);


		if (touchCube && interactKeyPressed) 
		{
			//Vector3 res = (-Vector3.right * Time.deltaTime * Input.GetAxis ("Horizontal") * speed);
			//Vector3 res2 = (-Vector3.forward * Time.deltaTime * Input.GetAxis ("Vertical") * speed);
			Vector3 res = (Vector3.right * Time.deltaTime * Input.GetAxis ("Horizontal") * speed);
			Vector3 res2 = (Vector3.forward * Time.deltaTime * Input.GetAxis ("Vertical") * speed);

			Vector3 otherPos = inContact.GetComponent<Rigidbody> ().position;

			Vector3 res3 = res + res2;
			res3 = transform.TransformDirection(res3);
			inContact.GetComponent<Rigidbody>().MovePosition(otherPos + res3);
		}

        if (touchSphere)
        {
            hasKey = true;
        }
        
        

        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        //Points arrow in direction of delivery point
        if(deliveryIndex < 3)
            arrow.transform.rotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), deliverLocations[deliveryIndex].position - arrow.transform.position);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "fogGate")
        {
            print("fogggggged");
            
            inFog = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "cube") 
		{
            groundContact = true;
			touchCube = true;
			isJumping = false;
			inContact = other.gameObject;
		}
        if(other.gameObject.name == "GateKey")
        {
            touchSphere = true;
            inContact = other.gameObject;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Ground")
        {
			isJumping = false;

            groundContact = true;
            //inContact = other.gameObject;
        }
        if (other.gameObject.name == "Gate")
        {
            atGate = true;
            inContact = other.gameObject;
        }
        if (other.gameObject.tag == "LargePatch")
        {
            groundContact = true;
            inContact = other.gameObject;
        }
        if (other.gameObject.tag == "SmallPatch")
        {
            groundContact = true;
            inContact = other.gameObject;
        }
    }

	void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "LargePatch")
        {
            groundContact = false;
            inContact = null;
        }
        if (other.gameObject.tag == "SmallPatch")
        {
            groundContact = false;
            inContact = null;
        }
        if (other.gameObject.name == "Gate")
        {
            atGate = false;
            inContact = null;
        }
		if (other.gameObject.tag == "cube") 
		{
			if (isJumping) {
				groundContact = false;
			}
            
			touchCube = false;
			inContact = null;
        }
        if (other.gameObject.name == "GateKey")
        {
            touchCube = false;
            inContact = null;
        }
        
        if (other.gameObject.tag == "Ground")
        {
            groundContact = false;
            //inContact = null;
        }
        if (other.gameObject.tag == "particles")
        {
            print("out");
            inFog = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Deliver"))
        {
            newspaperCount--;
            deliveryCount++;
            GameObject.Find("NewspaperCount").GetComponent<Text>().text = newspaperCount.ToString();
            GameObject.Find("DeliveryCount").GetComponent<Text>().text = deliveryCount.ToString();
            other.gameObject.SetActive(false);
            deliveryIndex++;          
        }     
    }
}
