using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Color minColor = Color.green;
    [SerializeField] private Color maxColor = Color.red;
    [SerializeField] private float maxPower = 400.0f;
    [SerializeField, Range(0,1)] private float powerMultiply = 0.6f;
    [SerializeField, Range(0,0.01f)] private float lineRendererLenghtMultiply = 0.005f;

    bool bIsSetStartPos = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 direction;
    float power;
    private void Start()
    {
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bIsSetStartPos = true;
            startPoint = Input.mousePosition;
            SetEnableLineRenderer(true);
        }
        if (bIsSetStartPos && Input.GetMouseButton(0))
        {
            endPoint = Input.mousePosition;
            DrawLine();
        }
        if (Input.GetMouseButtonUp(0) && bIsSetStartPos)
        {
            bIsSetStartPos = false;
            SetEnableLineRenderer(false);
            Throw();
        }
    }

    private void SetEnableLineRenderer(bool _Enable)
    {
        if (_Enable)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, rigidbody.position);
            lineRenderer.SetPosition(1, rigidbody.position);
            return;
        }
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 0;
    }

    private void DrawLine()
    {
        power = Vector3.Distance(startPoint, endPoint);
        power *= powerMultiply;
        if (power > maxPower)
            power = maxPower;
        direction = startPoint - endPoint;
        lineRenderer.SetPosition(0, rigidbody.position);
        lineRenderer.SetPosition(1, rigidbody.position + (new Vector3(direction.normalized.x, 0, direction.normalized.y) * power * lineRendererLenghtMultiply));
        float colorLerp = power / maxPower;
        lineRenderer.startColor = Color.Lerp(minColor, maxColor, colorLerp);
        lineRenderer.endColor = Color.Lerp(minColor, maxColor, colorLerp);
    }

    private void Throw()
    {
        power = Vector3.Distance(startPoint, endPoint);
        power *= powerMultiply;
        if (power > maxPower)
            power = maxPower;
        Debug.Log(power);
        direction = startPoint - endPoint;
        Vector3 temp = direction.normalized;
        direction = new Vector3(temp.x, 0, temp.y);
        rigidbody.AddForceAtPosition(direction * power, rigidbody.position);
    }
}
