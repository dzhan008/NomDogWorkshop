using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    public GameManager Manager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Treat")
        {
            if(Manager.GameState != State.End)
                Manager.EndGame();
        }
        Destroy(collision.gameObject);
    }
}
