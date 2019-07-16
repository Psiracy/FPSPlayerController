using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkSpeed, runSpeed, jumpPower;

    [SerializeField]
    Collider normalCollider, crounchCollider;

    [SerializeField]
    Transform cam;

    Vector3 camInitPos;
    float movementSpeed;
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camInitPos = cam.transform.localPosition;
    }

    void Update()
    {
        //input
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        //velocity
        Vector3 velocity = cam.transform.TransformDirection(movement);
        velocity.y = 0;

        //set speed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movementSpeed = runSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * jumpPower
, ForceMode.Impulse);
        }


        //crouch
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            normalCollider.enabled = false;
            crounchCollider.enabled = true;
            cam.transform.localPosition = new Vector3(camInitPos.x, -camInitPos.y / 4, camInitPos.z);//deviding the y axis by four and taking the minus of that gives the best results
            movementSpeed /= 2;
        }
        else
        {
            normalCollider.enabled = true;
            crounchCollider.enabled = false;
            cam.transform.localPosition = camInitPos;
        }

        //movement
        rigidbody.position += velocity * (movementSpeed * Time.deltaTime);

    }
}
