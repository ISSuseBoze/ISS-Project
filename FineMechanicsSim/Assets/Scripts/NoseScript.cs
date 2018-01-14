using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;


}

public class NoseScript : MonoBehaviour {


    public KeyCode transLeft = KeyCode.A;
    public KeyCode transRight = KeyCode.D;
    public KeyCode transUp = KeyCode.W;
    public KeyCode transDown = KeyCode.S;
    public KeyCode rotLeft = KeyCode.Q;
    public KeyCode rotRight = KeyCode.E;

    public float transForce = 10;
    public float rotForce = 5;

    private Rigidbody rb;

    public Boundary boundary;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();

        boundary.xMax = 9.5f;
        boundary.xMin = -10.0f;
        boundary.zMax = 10.0f;
        boundary.zMin = -10.0f;
    }
	
	// Update is called once per frame
	void Update () {

        moveByKeyPress();
        setToBounds();

    }

    //limit nose movement so it can't leave the camera scene
    private void setToBounds()
    {
        rb.position = new Vector3(
                            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                            0.0f,
                            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
    }


    //check if the nose should be moved on a pressed key
    private void moveByKeyPress()
    {
        if (Input.GetKey(transLeft))
        {
            rb.velocity = new Vector3(0, 0, transForce);
        }

        if (Input.GetKey(transRight))
        {
            rb.velocity = new Vector3(0, 0, -transForce);
        }

        if (Input.GetKey(transUp))
        {
            rb.velocity = new Vector3(transForce, 0, 0);
        }

        if (Input.GetKey(transDown))
        {
            rb.velocity = new Vector3(-transForce, 0, 0);
        }

        if (Input.GetKey(rotLeft))
        {
            rb.angularVelocity = new Vector3(0, -rotForce, 0);
        }

        if (Input.GetKey(rotRight))
        {
            rb.angularVelocity = new Vector3(0, rotForce, 0);
        }
    }
}
