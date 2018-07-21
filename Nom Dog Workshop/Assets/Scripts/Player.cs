using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Sprite IdleSprite;
    public Sprite EatingSprite;
    public Sprite CryingSprite;

    public GameManager Manager;

    bool eating = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Eat()
    {
        if(!eating)
        {
            eating = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = EatingSprite;
        }
    }

    public void Idle()
    {
        if(eating)
        {
            eating = false;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = IdleSprite;
    }

    public void Cry()
    {
        eating = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = CryingSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(eating)
        {
            if (collision.gameObject.tag == "Bone")
            {
                Manager.IncrementScore();
            }
            else if (collision.gameObject.tag == "Bomb")
            {
                Manager.GameEnd();
            }
            Destroy(collision.gameObject);
        }
    }
}
