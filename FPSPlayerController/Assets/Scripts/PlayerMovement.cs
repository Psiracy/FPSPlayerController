using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float crounchSpeedDivider = .6f;

    [SerializeField]
    Collider normalCollider, crouchCollider;

    [SerializeField]
    Transform cam;

    [SerializeField]
    float walkSpeed, runSpeed, jumpPower;

    Vector3 camInitPos;
    Rigidbody rigidBody;
    float movementSpeed;
    bool isGrounded, canCrouch;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        camInitPos = cam.transform.localPosition;
        isGrounded = true;
        canCrouch = true;
    }

    void Update()
    {
        //input
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        #region walking/running
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

        //crouch
        if (canCrouch == true && Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            normalCollider.enabled = false;
            crouchCollider.enabled = true;
            cam.transform.localPosition = new Vector3(camInitPos.x, -camInitPos.y / 4, camInitPos.z);//deviding the y axis by four and taking the minus of that, gives the best results
            movementSpeed *= crounchSpeedDivider;
        }
        else
        {
            normalCollider.enabled = true;
            crouchCollider.enabled = false;
            cam.transform.localPosition = camInitPos;
        }

        //movement
        rigidBody.position += velocity * (movementSpeed * Time.deltaTime);
        #endregion

        #region jump
        //check for ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, transform.localScale.y))
        {
            if (hit.transform != transform)
            {
                isGrounded = true;
                canCrouch = true;
            }
        }
        else
        {
            isGrounded = false;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rigidBody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            canCrouch = false;
        }
        #endregion
    }
}
