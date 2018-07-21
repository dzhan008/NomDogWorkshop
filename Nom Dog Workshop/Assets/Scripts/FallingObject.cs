using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    public int TorqueForce = -100;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddTorque(TorqueForce);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
