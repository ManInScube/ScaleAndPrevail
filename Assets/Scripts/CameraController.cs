using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 newPosition;
    private float panBorderThickness = 10f;
    [SerializeField]
    private float movementSpeed = 20f;
    [SerializeField]
    private float movementTime = 5f;


    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraInput();
    }

    private void HandleCameraInput()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += (Time.deltaTime * movementSpeed);
        }

        if (Input.GetKeyDown("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= (Time.deltaTime * movementSpeed);
        }

        if (Input.GetKeyDown("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= (Time.deltaTime * movementSpeed);
        }

        if (Input.GetKeyDown("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += (Time.deltaTime * movementSpeed);
        }

        //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.position = pos;
    }
}
