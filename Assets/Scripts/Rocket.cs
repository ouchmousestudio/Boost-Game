using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        processInput();
    }

    private void processInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            print("left pressed");
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            print("right pressed");
        }
    }
}
