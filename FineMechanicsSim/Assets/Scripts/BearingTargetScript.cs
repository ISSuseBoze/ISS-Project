using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearingTargetScript : MonoBehaviour {


    private Rigidbody rb;
    private MeshCollider mc;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when the nose fully enters the bearing
    private void OnTriggerStay(Collider other)
    {
        //i don't know how; i don't want to know how; i don't care; it just bloody works!
        if(mc.bounds.Contains(other.bounds.min) && mc.bounds.Contains(other.bounds.max) && other.gameObject.name == "Nose")
        {
            print("point is inside collider");
        }
            
    }
}
