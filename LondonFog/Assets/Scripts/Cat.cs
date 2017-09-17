using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    public float movementSpeed;
    public float lookDistance;
    public float targetDistance;
    public float dampling;
    public Transform target;
    public List<Point> path;
    Rigidbody rigidbody;
    Renderer renderer;

    private int index = 0;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.yellow;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetDistance = Vector3.Distance(target.position, transform.position);

        if (targetDistance < lookDistance)
        {
            LookAwayFromPlayer(path[index]);

            if (transform.position != path[index].position)
                Move(path[index]);
            else
            {
                if (index != path.Count - 1)
                    index++;
            }
        }
        else
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampling);
    }

    private void LookAwayFromPlayer(Point point)
    {
        Quaternion rotation = Quaternion.LookRotation(point.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampling);
    }

    private void Move(Point point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point.position, movementSpeed * Time.deltaTime);
    }
}