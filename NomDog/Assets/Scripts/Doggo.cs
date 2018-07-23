using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {

    public bool eating = false;

    public Sprite EatingSprite;
    public Sprite IdleSprite;
    public Sprite CryingSprite;

    private GameManager GameManager;

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Eat()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = EatingSprite;
        eating = true;
    }

    public void Idle()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = IdleSprite;
        eating = false;
    }

    public void Cry()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = CryingSprite;
        eating = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(eating)
        {
            if (collision.gameObject.tag == "Treat")
            {
                GameManager.IncrementScore();

            }
            else if (collision.gameObject.tag == "Bomb")
            {
                GameManager.EndGame();
            }
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (eating)
        {
            if (collision.gameObject.tag == "Treat")
            {
                GameManager.IncrementScore();

            }
            else if (collision.gameObject.tag == "Bomb")
            {
                Cry();
                GameManager.EndGame();
            }
            Destroy(collision.gameObject);
        }
    }
}
