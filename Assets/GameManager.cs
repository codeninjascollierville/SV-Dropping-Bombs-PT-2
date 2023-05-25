using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private Spawner spawner;
    public GameObject title;
    private Vector3 screenBounds;
    public GameObject playerPrefab;
    public GameObject splash;
    private GameObject player;
    private bool gameStarted = false;
    public GameObject scoreSystem;
    public Text scoreText;
    public int pointsWorth = 1;
    private int score;

    void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = playerPrefab;
        scoreText.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        title.SetActive(true);
        spawner.active = false;
        splash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
        {
            ResetGame();
        } 
        }
        else
        { 
            if (!player)
            {
            OnPlayerKilled();
            }


        }
        
        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");

        foreach (GameObject bombObject in nextBomb)
        {
            if(!gameStarted)
            {
                Destroy(bombObject);
            } else if(bombObject.transform.position.y < (-screenBounds.y) - 12 || !gameStarted)
            {
                scoreSystem.GetComponent<Score>().AddScore(pointsWorth);
                Destroy(bombObject);
            }
        }
    }

    void ResetGame()
    {
        title.SetActive(false);

        spawner.active = true;
        splash.SetActive(false);
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        gameStarted = true;
        

        scoreText.enabled = true;
        scoreSystem.GetComponent<Score>().score = 0;
        scoreSystem.GetComponent<Score>().Start();
    }


void OnPlayerKilled()
{
    splash.SetActive(true);
    spawner.active = false;
    gameStarted = false;

    


}



}