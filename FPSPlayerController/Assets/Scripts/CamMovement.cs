using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField, Range(0, 25)]
    float turningSpeed;

    float verticalCamRotation, horizontalCamRotation, xRotation;

    void Start()
    {
        //hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //cam horizontal rotation
        horizontalCamRotation = Input.GetAxis("Mouse X") * turningSpeed;
        transform.rotation *= Quaternion.Euler(0, horizontalCamRotation, 0);

        //cam vertical rotation
        verticalCamRotation = Input.GetAxis("Mouse Y") * (turningSpeed / 2); //half the verical rotation speed for a better result
        verticalCamRotation *= -1; //inverse the rotation
        xRotation += verticalCamRotation; //add to rotation
        Clamp(ref xRotation, -70, 70); //clamp the rotation
        Vector3 rot = new Vector3(xRotation, transform.rotation.eulerAngles.y, 0);
        transform.localRotation = Quaternion.Euler(rot);
    }

    void Clamp(ref float value, float min, float max)
    {
        if (value < min)
        {
            value = min;
        }
        else if (value > max)
        {
            value = max;
        }
    }
}
