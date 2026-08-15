using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 100f;
    public Transform player;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; ;
        float mouse_y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime; ;

        xRotation -= mouse_y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouse_x);
    }
}
