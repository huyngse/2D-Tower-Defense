using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float cameraSpeed = 5f;

    void Update() {
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
    }
}
