using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float _degreesPerSecond = 30f;
    [SerializeField] Vector3 _axis = Vector3.up;
    public string sceneName;
    public int par;
    void Update()
    {
        transform.Rotate(_axis.normalized * _degreesPerSecond * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            // Reset GameManager stuff
            GameManager.instance.tenSeconds = 10f;
            GameManager.instance.lives = 3;
            GameManager.instance.strokeCount = 0;
            GameManager.instance.liveCounter.ResetLives();
            SceneManager.LoadScene(sceneName);
        }
    }
}
