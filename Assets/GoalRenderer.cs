using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GoalRenderer : MonoBehaviour
{
    public Transform target;
    public float lineLength = 3f;
    public float arrowLength = 1f;
    public float arrowAngle = 20f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            // Set the first point of the line renderer to the current position of this object
            lineRenderer.SetPosition(0, transform.position);

            // Set the second point of the line renderer to the target position, clamped to a maximum distance of lineLength
            lineRenderer.SetPosition(1, Vector3.ClampMagnitude(target.position - transform.position, lineLength) + transform.position);

            Vector3 dir = target.position - transform.position;
            Vector3 arrowPos = target.position - (dir.normalized * arrowLength);

            Quaternion arrowRot = Quaternion.LookRotation(-dir);
            Vector3 arrowTip = arrowPos + arrowRot * Vector3.forward * arrowLength;
            Vector3 arrowLeft = arrowPos + arrowRot * Quaternion.Euler(0, arrowAngle, 0) * Vector3.back * arrowLength;
            Vector3 arrowRight = arrowPos + arrowRot * Quaternion.Euler(0, -arrowAngle, 0) * Vector3.back * arrowLength;

            lineRenderer.material.SetPass(0);
            GL.Begin(GL.TRIANGLES);
            GL.Vertex(arrowTip);
            GL.Vertex(arrowLeft);
            GL.Vertex(arrowRight);
            GL.End();
        }
    }
}
