using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeParentScript : MonoBehaviour
{
    public Transform player;
    public float slimeSpeed = 1f;
    private bool follow = false;
    private bool idle = true;
    public float aggro = 15f;
    private int wpHead = 0;
    private Transform headTo;
    private float wayProx = .2f;
    private Transform[] wayList;
    // Start is called before the first frame update
    void Start()
    {
        wayList = transform.parent.GetComponentInChildren<waypointSetup>().points;
        headTo = wayList[wpHead];
    }

    // Update is called once per frame
    void Update()
    {
        moveSlime();
        checkAggro();
    }

    private void checkAggro()
    {
        if(Vector3.Distance(transform.position, player.position) <= aggro)
        {
            follow = true;
            idle = false;
        }
        else
        {
            follow = false;
            idle = true;
        }
    }

    private void moveSlime()
    {
        if (follow)
        {
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, slimeSpeed * Time.deltaTime);
        }
        else if (idle)
        {
            if (Vector3.Distance(transform.position, headTo.position) < wayProx)
            {
                GetNextWaypoint();
            }
            transform.position = Vector3.MoveTowards(transform.position, headTo.position, slimeSpeed * Time.deltaTime);
        }
    }

    private void GetNextWaypoint()
    {
        if(wpHead + 1 < wayList.Length)
        {
            wpHead += 1;
        }
        else
        {
            wpHead = 0;
        }
        headTo = wayList[wpHead];
    }
}
