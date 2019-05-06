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
    public float jumpExe = 3f;
    private bool canJump = true;
    public float groundDist;
    public float extraGrdDist = 10.5f;
    public float fallMult = 1.8f;
    public float lowMult = 1.8f;
    private Animator anim;
    public float dead = .1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundDist = GetComponentInChildren<BoxCollider>().bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void JumpPlayer()
    {
        bool grdDist = Physics.Raycast(transform.position, Vector3.down, groundDist + extraGrdDist);
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMult * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowMult * Time.deltaTime;
        }
        if (!canJump && rb.velocity.y <= 0)
        {
            anim.SetBool("jump", false);
            canJump = grdDist;
            Debug.Log(canJump.ToString());
        }
        if (Input.GetButtonDown("Jump") && canJump)
        {
            anim.SetBool("jump", true);
            canJump = false;
        }
    }

    private void AddJump()
    {
            rb.AddForce(Vector3.up * jumpExe, ForceMode.VelocityChange);
    }

    private void FastAnim()
    {
        anim.speed = 1.5f;
    }

    private void NormalAnim()
    {
        anim.speed = 1f;
    }

    private void MovePlayer()
    {
        float hMov = Input.GetAxis("Horizontal");
        float vMov = Input.GetAxis("Vertical");
        if(Mathf.Abs(hMov) > dead || Mathf.Abs(vMov) > dead)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        Vector3 moveDirection = new Vector3(hMov * moveSpeed, 0f, vMov * moveSpeed);

        Vector3 forward = ypivot.transform.TransformDirection(moveDirection);

        Vector3 facing = new Vector3(forward.x, 0f, forward.z);
        Vector3 lookRot = Vector3.RotateTowards(transform.forward, facing, rotSpeed, 0f);
        rb.MoveRotation(Quaternion.LookRotation(lookRot));

        rb.MovePosition(transform.position + facing * Time.deltaTime);
    }
}
    