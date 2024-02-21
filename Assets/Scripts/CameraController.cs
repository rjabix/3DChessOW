using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 7.5f;
    public float rotationSpeed = 500f;
    public float zoomSpeed = 5f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 20f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            Vector3 currentRotation = transform.eulerAngles;

            currentRotation.z = 0f;

            transform.eulerAngles = currentRotation;

            transform.Rotate(Vector3.up * mouseX);
            transform.Rotate(-Vector3.right * mouseY);
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float zoomAmount = scrollInput * zoomSpeed;

            Vector3 cameraDirection = transform.forward;

            Vector3 newPosition = transform.position + cameraDirection * zoomAmount;

            float newZoomDistance = Mathf.Clamp(newPosition.y, minZoomDistance, maxZoomDistance);
            newPosition.y = newZoomDistance;

            transform.position = newPosition;
        }
    }
}
