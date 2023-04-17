using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float tenSeconds = 10f;
    private float lastTime;

    public int strokeCount = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI strokeText;

    public GameObject respawnPoint;

    public GameObject ball;

    public int lives = 3;

    public GameObject UI;
    public GameObject tutorialObject;

    public LiveCounter liveCounter;

    public bool tutorial;

    public int par;

    public Par parScript;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            tutorial = true;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(UI);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lastTime = Time.time;
        ball = GameObject.FindGameObjectWithTag("Player");
        liveCounter = UI.GetComponentInChildren<LiveCounter>();
        parScript = UI.GetComponentInChildren<Par>();
        parScript.gameObject.SetActive(false);
    }

    private void Update()
    {
        timerText.text = tenSeconds.ToString("F1");
        strokeText.text = strokeCount.ToString();

        if (tutorial)
        {
            if (Input.GetMouseButtonUp(0))
            {
                tutorial = false;
                tutorialObject.SetActive(false);
                parScript.PrintPar();
                parScript.gameObject.SetActive(true);
            }
        }

        if (GameObject.FindGameObjectWithTag("BlackHole"))
        {
            par = GameObject.FindGameObjectWithTag("BlackHole").GetComponent<BlackHole>().par;
        }

        parScript.par = par;

        if(strokeCount >= par)
        {
            LoseLife();
        }
    }

    public void DecrementClock()
    {
        float elapsedTime = Time.time - lastTime;
        if (elapsedTime > 1f)
        {
            tenSeconds --;
            lastTime = Time.time;
        }
        if(tenSeconds <= 0)
        {
            ball.GetComponent<GolfBomb>().BlowUp();
            tenSeconds = 10f;
        }
    }

    public void LoseLife()
    {
        lives--;
        if (lives == 0) // Game over
        {
            Destroy(UI);
            SceneManager.LoadScene("LoseScene");
        }
        else
        {
            strokeCount = 0;
            tenSeconds = 10f;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.position = ball.GetComponent<GolfBomb>().respawnPoint.transform.position;
            //ball.SetActive(true);
        }
    }

    public void FindBall()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
    }

}
