using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
    private Rigidbody rb;
    public Transform ypivot;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void JumpPlayer()
    {

    }

    private void MovePlayer()
    {
        float hMov = Input.GetAxis("Horizontal");
        float vMov = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(hMov * moveSpeed, 0f, vMov * moveSpeed);

        Vector3 forward = ypivot.transform.TransformDirection(moveDirection);

        Vector3 facing = new Vector3(forward.x, 0f, forward.z);
        Vector3 lookRot = Vector3.RotateTowards(transform.forward, facing, rotSpeed, 0f);
        rb.MoveRotation(Quaternion.LookRotation(lookRot));

        rb.MovePosition(transform.position + facing * Time.deltaTime);
    }
}
