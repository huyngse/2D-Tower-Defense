using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float cameraSpeed = 5f;
    private float maxX;
    private float minY;

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.right);
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 0, maxX),
            Mathf.Clamp(transform.position.y, minY, 0),
            -10
        );
    }

    public void SetLimits(Vector3 bottomLeftTile)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        maxX = bottomLeftTile.x - worldPoint.x;
        minY = bottomLeftTile.y - worldPoint.y;
    }
}
