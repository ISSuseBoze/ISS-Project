using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseScript : MonoBehaviour {


    public KeyCode transLeft = KeyCode.A;
    public KeyCode transRight = KeyCode.D;
    public KeyCode transUp = KeyCode.W;
    public KeyCode transDown = KeyCode.S;
    public KeyCode rotLeft = KeyCode.Q;
    public KeyCode rotRight = KeyCode.E;

    public float transForce = 15;
    public float rotForce = 10;

    private Rigidbody rb;


    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();


    }
	
	// Update is called once per frame
	void Update () {

        

        if (Input.GetKey(transLeft))
        {
            rb.velocity = new Vector3(-transForce, 0, 0);
        }

        if (Input.GetKey(transRight))
        {
            rb.velocity = new Vector3(transForce, 0, 0);
        }

        if (Input.GetKey(transUp))
        {
            rb.velocity = new Vector3(0, 0, transForce);
        }

        if (Input.GetKey(transDown))
        {
            rb.velocity = new Vector3(0, 0, -transForce);
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
