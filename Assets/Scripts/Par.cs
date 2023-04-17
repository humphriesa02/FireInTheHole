using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Par : MonoBehaviour
{
    public bool isActive;
    public float timeActive = 5f;
    private TextMeshProUGUI text;
    public int par;
    private void Start()
    {
        isActive = true;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (isActive && timeActive > 0)
        {
            timeActive -= Time.deltaTime;
        }
        else if(isActive && timeActive <= 0)
        {
            isActive = false;
        }

        if (isActive)
        {
            int actualPar = par - 1;
            text.text = "Par: " + actualPar.ToString();
        }
        else if(!isActive)
        {
            gameObject.SetActive(false);
        }
    }
    public void PrintPar()
    {
        isActive = true;
        timeActive = 5f;
    }
}
