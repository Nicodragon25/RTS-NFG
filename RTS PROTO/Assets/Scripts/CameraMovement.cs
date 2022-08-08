using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    GameObject cam;

    public GameObject rotatePointBox;
    public float speed;
    public float mouseSensitivityX;
    public float mouseSensitivityY;


    float xRotation = 25f;
    float distance;

    int zoomQuantity;
    private void Start()
    {
        cam = Camera.main.gameObject;
        zoomQuantity = 5;
    }
    void Update()
    {
        if(Input.GetKey("w")) transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Input.GetKey("s")) transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(Input.GetKey("a")) transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(Input.GetKey("d")) transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift)) speed = speed * 1.5f;
        if (Input.GetKeyUp(KeyCode.LeftShift)) speed = speed / 1.5f;
        float ordenadaOrigen = 6.25f * zoomQuantity - 2.75f;
        float fov = 0.46f * xRotation + ordenadaOrigen;

        cam.GetComponent<Camera>().fieldOfView = fov;

        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 25f, 90f);

            distance = 20 / (Mathf.Tan(xRotation * Mathf.Deg2Rad));
            cam.transform.localPosition = new Vector3(0, 0, -distance);
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(zoomQuantity < 10) zoomQuantity += 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(zoomQuantity > 1) zoomQuantity -= 1;
        }
    }
}
