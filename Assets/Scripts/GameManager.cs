using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager S;

    [Header("Set in Inspector")]
    public GameObject BeePrefab;
    public GameObject BeeSpawnPoint;
    public Text Text_Lives; // Text GameObject that displays lives.
    public Text Text_Timer; // Text GameObject that displays time.
    public Text Text_Points; // Text GameObject that displays points.
    public Canvas Canvas_LevelComplete; // Canvas GameObject that displays when the level is complete.
    public Canvas Canvas_LevelLost; // Canvas GameObject that displays when the level is lost.

    public Vector3 startPos = Vector3.zero; // Starting position for the bee on the level.
    public int lives = 3; // Number of lives the player has.
    public int points = 0; // Number of points the player has.
    public float timer = 20; // Timer.

    [Header("Set Dynamically")]
    public List<GameObject> balloons; // List that contains all balloons present in the level.
    public bool isTimerActive = false;


    private void Awake()
    {
        S = this; // Initialize singleton.        
    }
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if( isTimerActive )
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.Floor(timer % 60);
            Text_Timer.text = "Time: " + minutes + ":" + seconds;
            if (seconds >= 10F)
            {
                Text_Timer.text = "Time: " + minutes + ":" + seconds;
            }
            else if (seconds < 10F)
            {
                Text_Timer.text = "Time: " + minutes + ":" + "0" + seconds ;
            } else
            {
                Text_Timer.text = "Time: " + minutes + ":00";
            }

            if (minutes == 0F && seconds == 0F)
            {
                isTimerActive = false;
                SceneManager.LoadScene("_End_Screen");
            }
        }

        // Once all balloons are destroyed, the level is won.
        if( balloons.Count == 0 )
        {
            LevelComplete(true);
            isTimerActive = false;
        }

        // Once the player is out of lives, the level is lost.
        if( lives <= 0 )
        {
            LevelComplete(false);
            isTimerActive = false;
        }

    }

    private void StartLevel()
    {
        isTimerActive = true;
        Instantiate(BeePrefab);
        InitializeBalloons();
    }

    private void LevelComplete(bool didWinOrLose)
    {
        if( didWinOrLose )
        {
            Canvas_LevelComplete.gameObject.SetActive(true);
        } else
        {
            Canvas_LevelLost.gameObject.SetActive(true);
        }
    }

    private void InitializeBalloons()
    {
        GameObject[] balloonArray = GameObject.FindGameObjectsWithTag("Balloon");
        foreach (GameObject balloon in balloonArray)
        {
            Color c = new Color(Random.value, Random.value, Random.value);
            Renderer balloonRenderer = balloon.GetComponent<Renderer>();
            balloonRenderer.material.color = c;
            balloons.Add(balloon);
        }
    }

    public void BalloonPopped(string name)
    {
        // Searches through the balloons list to find a specific balloon.
        // That balloon is then removed from the list and destroyed.f
        foreach(GameObject balloon in balloons)
        {
            if (balloon.name == name)
            {
                balloons.Remove(balloon);
                Destroy(balloon);
                AddPoints(1);
                break;
            }
        }
    }

    // Function used to decrement the lives variable.
    // Can be called in other classes.
    public void LoseLife()
    {
        lives--;
        Text_Lives.text = "Lives: " + lives;
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Text_Points.text = "Points: " + points;
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
    }
}


// Config:
// Add "Balloon" and "RedZone" layers in project settings
// Add all scenes to File>Build Settings>Setting in Build

//**********************************************************

// TODO:

// - Create 4 levels
//      - Create more empty gameobjects like "Level_01" in _Scene_0 
//      - or
//      - Make a new scene for each level

// - Create a pause button

// - add "brief introduction how the game is played, developer names, a start button, as well as the
//   levels the player last played" to start scene
//      - last levels could be dont with a playerPref

// "Environment has a distinct theme, with each element is purposeful and meaningful for the scene"






