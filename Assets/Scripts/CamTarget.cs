using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTarget : MonoBehaviour
{
    public GameObject ball;

    private void Start()
    {
        if(ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void Update()
    {
        transform.position = ball.transform.position;
    }
}
