using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public int leftMostTile;
    public int rightMostTile;
    public int highestTile;
    public int lowestTile;

    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        pos.z = -8;

        if(pos.x >= leftMostTile + 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pos.x -= 1;
                transform.position = pos;
            }
        }

        if(pos.x <= rightMostTile - 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pos.x += 1;
                transform.position = pos;
            }
        }

        if(pos.y <= highestTile - 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pos.y += 1;
                transform.position = pos;
            }
        }

        if(pos.y >= lowestTile + 1)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                pos.y -= 1;
                transform.position = pos;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D unit)
    {
        Debug.Log("Detected");
    }
}
