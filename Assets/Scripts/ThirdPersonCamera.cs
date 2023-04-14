using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target; //the transform of the target object (the bomb)
    public float distance = 10.0f; //the distance between the camera and the target
    public float height = 2.0f; //the height of the camera above the target
    public float smoothSpeed = 1; //the smoothness of camera movement
    public float rotationSpeed = 1.0f; //the speed of camera rotation

    private float mouseX; //the horizontal mouse input
    private float mouseY; //the vertical mouse input

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; //get the horizontal mouse input and apply it to the mouseX variable
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed; //get the vertical mouse input and apply it to the mouseY variable
        mouseY = Mathf.Clamp(mouseY, -45.0f, 45.0f); //clamp the mouseY value between -45 and 45 degrees
        distance -= Input.mouseScrollDelta.y;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + Quaternion.Euler(mouseY, mouseX, 0.0f) * new Vector3(0.0f, height, -distance); //calculate the target position using the target's position, the mouse inputs, and the camera's distance and height from the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime); //move the camera smoothly to the target position
        transform.LookAt(target); //make the camera look at the target
    }
}