using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    [Header("Set in Inspector")]
    //public GameObject BeePrefab;
    public Text Text_Lives;
    public Vector3 startPos = Vector3.zero;
    public int lives = 3;

    [Header("Set Dynamically")]
    public List<GameObject> balloons;

    private void Awake()
    {
        S = this;
        GameObject[] balloonArray = GameObject.FindGameObjectsWithTag("Balloon");
        foreach (GameObject balloon in balloonArray)
        {
            balloons.Add(balloon);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartLevel()
    {
        //Instantiate(BeePrefab);
    }

    public void BalloonPopped(string name)
    {
        foreach(GameObject balloon in balloons)
        {
            if (balloon.name == name)
            {
                balloons.Remove(balloon);
                Destroy(balloon);
                break;
            }
        }
    }

    public void LoseLife()
    {

    }
}
