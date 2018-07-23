using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    //The torque value you want to add onto the falling object.
    public float torque = 150f;
    public float velocity = -8f;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddTorque(torque);
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, velocity, 0f);
		
	}
}
