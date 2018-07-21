using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Player Left_Dog;
    public Player Right_Dog;

    private int Score = 0;
    public Text ScoreText;

    public Spawner GameSpawner;

    //UI Elements
    public GameObject TitleScreen;
    public GameObject RetryScreen;

    private bool GameOver = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(!GameOver)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (hitInfo)
                    {
                        if (hitInfo.transform.gameObject.tag == "Corgi")
                        {
                            Left_Dog.Eat();
                        }
                        else if (hitInfo.transform.gameObject.tag == "Shiba")
                        {
                            Right_Dog.Eat();
                        }
                    }
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    if (hitInfo.transform.gameObject.tag == "Corgi")
                    {
                        Left_Dog.Idle();
                    }
                    else if (hitInfo.transform.gameObject.tag == "Shiba")
                    {
                        Right_Dog.Idle();
                    }
                }

            }
            if (Input.GetButton("Left_Dog"))
            {
                Left_Dog.Eat();
            }
            else if (Input.GetButtonUp("Left_Dog"))
            {
                Left_Dog.Idle();
            }

            if (Input.GetButton("Right_Dog"))
            {
                Right_Dog.Eat();
            }
            else if (Input.GetButtonUp("Right_Dog"))
            {
                Right_Dog.Idle();
            }
        }

		
	}

    public void OnPlay()
    {
        GameOver = false;
        GameSpawner.Activate();
        TitleScreen.SetActive(false);
        ScoreText.gameObject.SetActive(true);
    }

    public void OnRetry()
    {
        GameOver = false;
        GameSpawner.Activate();
        Score = 0;
        ScoreText.text = "0";
        Left_Dog.Idle();
        Right_Dog.Idle();
        RetryScreen.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void IncrementScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }

    public void GameEnd()
    {
        GameOver = true;
        Left_Dog.Cry();
        Right_Dog.Cry();
        GameSpawner.DeActivate();
        RetryScreen.SetActive(true);
    }
}
