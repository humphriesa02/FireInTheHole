using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float tenSeconds = 10f;
    private float lastTime;

    public int strokeCount = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI strokeText;

    public GameObject ball;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    private void Update()
    {
        timerText.text = tenSeconds.ToString("F1");
        strokeText.text = strokeCount.ToString();
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
        }
    }
}
