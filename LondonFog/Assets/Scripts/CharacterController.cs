using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    public Rigidbody rb;
    // Use this for initialization
    void Start () {
        speed = 5;
        sensitivity = 10;
        rb = GetComponent<Rigidbody>();
       
    }
	
	// Update is called once per frame
	void Update () {
        

        //smooth jumping animation
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y<=1.2)
        {
            rb.AddForce(transform.up * (speed - rb.velocity.y), ForceMode.Impulse);
        }


        //Moves Forward and back along z axis                           //Up/Down
        transform.Translate(-Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);

        //Moves Left and right along x Axis                               //Left/Right
        transform.Translate(-Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
        
    }
}
