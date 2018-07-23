using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Menu = 0,
    Playing,
    End,
    Debug
}

public class GameManager : MonoBehaviour {

    public State GameState = State.Menu;
    public AudioSource AudioManager;

    public Doggo Corgi;
    public Doggo Shiba;

    private int CorgiTouchIndex = -1;
    private int ShibaTouchIndex = -1;

    private int Score = 0;
    private int HighScore = 0;
    public Spawner Spawner;
    public AutoRotator Rotator;

    private bool GameEnd = false;
    private bool HustleMode = false;

    //UI Elements
    public GameObject TitleScreen;
    public GameObject RetryScreen;
    public GameObject CreditsScreen;
    public GameObject InstructionsScreen;

    public Text HighScoreText;
    public Text ScoreText;

    public GameObject HustleModeButton;
    public Text HustleModeText;
    private Color HustleModeTextColor;

    private String GameDataPath;

    //Music
    private AudioClip DoggoHustle;
    private AudioClip NomDogSong;
    // Use this for initialization
    void Start() {
        GameDataPath = Application.persistentDataPath + "/gameInfo.dat";
        Load();

        GameState = State.Menu;
        if (GameState == State.Debug)
        {
            OnPlay();
        }

        DoggoHustle = (AudioClip)Resources.Load("Music/DoggoHustle");
        NomDogSong = (AudioClip)Resources.Load("Music/NomDog");

        HustleModeTextColor = HustleModeText.color;
        HustleModeText.color = Color.clear;

    }

    public void OnPlay()
    {
        if (HustleMode)
        {
            AudioManager.clip = DoggoHustle;
            AudioManager.Play();
            Rotator.Activate();
        }
        else
        {
            if(AudioManager.clip == DoggoHustle)
            {
                AudioManager.clip = NomDogSong;
                AudioManager.Play();
            }
        }
        GameState = State.Playing;
        HustleModeButton.SetActive(false);
        Spawner.Activate(HustleMode);
        ScoreText.gameObject.SetActive(true);
        TitleScreen.SetActive(false);
    }

    public void OnCredits()
    {
        TitleScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void OnInstructions()
    {
        TitleScreen.SetActive(false);
        InstructionsScreen.SetActive(true);
    }

    public void OnBack()
    {
        if(CreditsScreen.activeInHierarchy)
            CreditsScreen.SetActive(false);
        if(InstructionsScreen.activeInHierarchy)
            InstructionsScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }

    public void OnRetry()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        OnPlay();
        Spawner.ClearObjects();
        Score = 0;
        ScoreText.text = "0";
        Shiba.Idle();
        Corgi.Idle();
        RetryScreen.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void ToggleHustleMode()
    {
        HustleMode = HustleMode ? false : true; //If true, make false. If not true (false), make true
        if (!HustleMode)
        {
            HustleModeButton.GetComponent<Image>().color = Color.clear;
            HustleModeText.color = Color.clear;
        }
        else
        {
            HustleModeButton.GetComponent<Image>().color = Color.white;
            HustleModeText.color = HustleModeTextColor;
        }
    }

    // Update is called once per frame
    void Update () {
        if(GameState == State.Playing)
        {
            for (var i = 0; i < Input.touchCount; ++i)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
                {

                    // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
                    if (hitInfo)
                    {
                        if(hitInfo.transform.gameObject.tag == "Corgi")
                        {
                            CorgiTouchIndex = i;
                            Corgi.Eat();
                        }
                        else if(hitInfo.transform.gameObject.tag == "Shiba")
                        {
                            ShibaTouchIndex = i;
                            Shiba.Eat();
                        }
                        else
                        {
                            if(i == CorgiTouchIndex)
                            {
                                CorgiTouchIndex = -1;
                                Corgi.Idle();
                            }
                            else if(i == ShibaTouchIndex)
                            {
                                ShibaTouchIndex = -1;
                                Shiba.Idle();
                            }
                        }
                        // Here you can check hitInfo to see which collider has been hit, and act appropriately.
                    }
                    else
                    {
                        if (i == CorgiTouchIndex)
                        {
                            CorgiTouchIndex = -1;
                            Corgi.Idle();
                        }
                        else if (i == ShibaTouchIndex)
                        {
                            ShibaTouchIndex = -1;
                            Shiba.Idle();
                        }
                    }
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    if (hitInfo.transform.gameObject.tag == "Corgi")
                    {
                        Corgi.Idle();
                    }
                    else if (hitInfo.transform.gameObject.tag == "Shiba")
                    {
                        Shiba.Idle();
                    }
                }
            }

            if (Input.GetButtonDown("Corgi_Eat"))
            {
                Corgi.Eat();
            }
            else if (Input.GetButtonUp("Corgi_Eat"))
            {
                Corgi.Idle();
            }

            if (Input.GetButtonDown("Shiba_Eat"))
            {
                Shiba.Eat();
            }
            else if (Input.GetButtonUp("Shiba_Eat"))
            {
                Shiba.Idle();
            }
        }
	}

    public void IncrementScore()
    {
        Score++;
        ScoreText.text = Score.ToString();

        if (Score % 10 == 0 && Score != 0 && Spawner.SPAWN_RATE != 0.05f)
        {
            Spawner.SPAWN_RATE -= 0.03f;
            Spawner.MIN_SPEED += 0.3f;
            Spawner.MAX_SPEED += 0.3f;
        }
    }

    //End the game, disabling the spawner and setting the sprites of the dogs.
    public void EndGame()
    {
        if(HustleMode)
        {
            Rotator.DeActivate();
        }

        Corgi.Cry();
        Shiba.Cry();
        Spawner.DeActivate();
        RetryScreen.SetActive(true);
        HustleModeButton.SetActive(true);
        GameState = State.End;
        UpdateHighScore();
        HighScoreText.text = "High Score: " + HighScore.ToString();
        Save();
    }

    void UpdateHighScore()
    {
        if(HighScore < Score)
        {
            HighScore = Score;
        }
    }

    public void Save()
    {
        //Created to open up a data file containing game data
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GameDataPath);

        //Construct the object for game data
        GameData data = new GameData();
        Debug.Log(HighScore);
        data.highscore = HighScore;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(GameDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GameDataPath, FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            HighScore = data.highscore;
        }
    }
}

[Serializable]
class GameData
{
    public int highscore;
}
