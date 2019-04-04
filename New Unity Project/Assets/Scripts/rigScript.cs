using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigScript : MonoBehaviour
{
    public Transform player;
    public Transform ypivot;
    public Transform xpivot;
    public float maxRot = 45;
    public float minRot = -20;
    public float rotSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pivotRotation();
    }

    private void pivotRotation()
    {
        transform.position = player.position;
        float hRot = Input.GetAxis("rHorizontal");
        float vRot = Input.GetAxis("rVertical");
        Vector3 yRot = ypivot.transform.eulerAngles;
        Vector3 xRot = xpivot.transform.eulerAngles;
        yRot.y += hRot * rotSpeed;
        xRot.x += vRot * rotSpeed;
        xRot.y = yRot.y;
        ypivot.transform.eulerAngles = yRot;
        xpivot.transform.eulerAngles = xRot;
        if (xpivot.eulerAngles.x > maxRot && xpivot.eulerAngles.x < 180f)
        {
            xpivot.rotation = Quaternion.Euler(maxRot, xpivot.eulerAngles.y, xpivot.eulerAngles.z);
        }
        if (xpivot.eulerAngles.x < 360f+minRot && xpivot.eulerAngles.x > 180f)
        {

            xpivot.rotation = Quaternion.Euler(360f+minRot, xpivot.eulerAngles.y, xpivot.eulerAngles.z);
        }
    }
}
