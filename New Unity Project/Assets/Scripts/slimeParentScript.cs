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
    public float aggro = 2f;
    private int wpHead = 0;
    private Transform headTo;
    private float wayProx = .2f;
    private Transform[] wayList;
    // Start is called before the first frame update
    void Start()
    {
        wayList = transform.GetComponentInChildren<waypointSetup>().points;
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
        throw new NotImplementedException();
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
            Vector3 direction = headTo.position - transform.position;
            if (Vector3.Distance(transform.position, headTo.position) < wayProx)
            {
                GetNextWaypoint();
            }
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
