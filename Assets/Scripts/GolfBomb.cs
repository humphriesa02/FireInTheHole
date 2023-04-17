using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBomb : MonoBehaviour
{

    public float maxHitPower = 20.0f; //the maximum power of the hit
    public Rigidbody rb; //reference to the Rigidbody component
    public LineRenderer lineRenderer; //reference to the LineRenderer component
    public float lineScale = 4;

    private float hitPower; //the current power of the hit
    private bool isCharging; //whether the player is currently charging the shot
    private Vector3 direction = Vector3.zero;
    private bool canHit;

    public GameObject explosion;

    public float lineRendererOffset = 1f;

    public GameObject respawnPoint;

    public AudioSource audioSource;

    public AudioClip explosionSound;

    public AudioClip hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //get the reference to the Rigidbody component
        lineRenderer = GetComponent<LineRenderer>(); //get the reference to the LineRenderer component
        lineRenderer.enabled = true; //disable the LineRenderer at the start
        lineRenderer.sortingOrder = 1;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.red;
        canHit = true;
        respawnPoint = GameObject.FindGameObjectWithTag("Re");
        GameManager.instance.FindBall();
        if (!GameManager.instance.tutorial)
        {
            GameManager.instance.parScript.PrintPar();
        }
    }

    void Update()
    {
        if (!GameManager.instance.tutorial)
        {
            if (Input.GetMouseButtonDown(0) && canHit)
            { //if the spacebar is pressed
                isCharging = true; //start charging the shot
                direction = GetPlayerDirection(); //get the direction from the player
            }

            if (Input.GetMouseButtonUp(0) && canHit)
            { //if the spacebar is released
                audioSource.PlayOneShot(hitSound);
                isCharging = false; //stop charging the shot
                HitBall(direction, hitPower); //call the HitBall function with the direction and hit power
                hitPower = 0.0f; //reset the hit power
                direction = Vector3.zero;
            }

            if (Vector3.Distance(rb.velocity, Vector3.zero) > 0.5)
            {
                canHit = false;
            }
            else if (Vector3.Distance(rb.velocity, Vector3.zero) < 0.5)
            {
                GameManager.instance.DecrementClock();
                rb.velocity = Vector3.zero;
                canHit = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                BlowUp();
            }


            if (isCharging)
            { //if the player is charging the shot
                hitPower = Mathf.Min(hitPower + Time.deltaTime * maxHitPower, maxHitPower); //increase the hit power up to the maximum
            }

            DrawLine(direction);
        } 
    }

    void HitBall(Vector3 direction, float power)
    {
        print(direction);
        rb.AddForce(direction * power, ForceMode.Impulse);
        GameManager.instance.strokeCount++;
    }

    Vector3 GetPlayerDirection()
    {
        Vector3 cameraForward = Camera.main.transform.forward; // get the forward direction of the camera
        direction = cameraForward.normalized; // set the direction to the normalized camera forward vector
        return direction;
    }

    void DrawLine(Vector3 direction)
    {
        Vector3[] positions = { transform.position, transform.position + (direction * (hitPower / maxHitPower)) * lineScale }; //create an array of two positions, scaled by the current hit power
        positions[0].y += lineRendererOffset;
        positions[1].y += lineRendererOffset;
        lineRenderer.positionCount = 2; //set the number of positions to two
        lineRenderer.SetPositions(positions); //set the positions of the LineRenderer
    }

    public void BlowUp()
    {
        audioSource.PlayOneShot(explosionSound);
        Instantiate(explosion, transform.position, Quaternion.identity);
        //gameObject.SetActive(false);
        GameManager.instance.LoseLife();
    }
}
