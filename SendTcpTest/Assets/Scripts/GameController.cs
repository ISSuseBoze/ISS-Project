using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private int rnd;
    private int cnt = 0;
    private Client client;

    // Use this for initialization
    void Start () {
        client = new Client("127.0.0.1", 1111);


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rnd = (int)UnityEngine.Random.Range(0, 10);
        
        string msg = "";
        
        if (rnd >= 5)
        {
            cnt++;
            
            msg = cnt.ToString() + "," + rnd.ToString() + "," + new System.Random().NextDouble().ToString();

            client.SendMsg(msg);
            Console.WriteLine(msg);
        }
        
        
	}
}
