using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveCounter : MonoBehaviour
{
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;
    private void Update()
    {
        if(GameManager.instance.lives == 0)
        {
            live1.SetActive(false);
            live2.SetActive(false);
            live3.SetActive(false);
        }
        else if(GameManager.instance.lives == 1)
        {
            live1.SetActive(true);
            live2.SetActive(false);
            live3.SetActive(false);
        }
        else if (GameManager.instance.lives == 2)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(false);
        }
        else if (GameManager.instance.lives == 3)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
        }
    }

    public void ResetLives()
    {
        live1.SetActive(true);
        live2.SetActive(true);
        live3.SetActive(true);
    }
}
