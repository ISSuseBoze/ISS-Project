using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearingTargetScript : MonoBehaviour {


    private Rigidbody rb;
    private MeshCollider mc;
    private bool noseMovementStarted;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        noseMovementStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when the nose fully enters the bearing
    private void OnTriggerStay(Collider other)
    {
        //i don't know how; i don't want to know how; i don't care; it just bloody works!
        if(noseMovementStarted && mc.bounds.Contains(other.bounds.min) && mc.bounds.Contains(other.bounds.max) && other.gameObject.name == "Nose")
        {
            print("Nose is IN");
        }
            
    }

    //when the nose first leaves the bearing due to a random force
    private void OnTriggerExit(Collider other)
    {
        if (!(noseMovementStarted && mc.bounds.Contains(other.bounds.min) && mc.bounds.Contains(other.bounds.max)) && other.gameObject.name == "Nose")
        {
            print("Nose just left");
            noseMovementStarted = true;

        }
    }
}
