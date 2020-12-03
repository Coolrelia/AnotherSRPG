using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public int highestPoint;
    public int lowestPoint;

    public GameObject highlight;
    private Vector3 highlightPos;

    public GameObject endTurn;

    public GameMaster gm;

    private void Start()    
    {
        gm = FindObjectOfType<GameMaster>();
        highlight = GameObject.FindGameObjectWithTag("Highlight");
        highlightPos = highlight.GetComponent<RectTransform>().localPosition;
    }

    private void Update()
    {
        if(highlightPos == endTurn.GetComponent<RectTransform>().localPosition)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gm.EndTurn();
            }
        }

        if (highlightPos.y <= highestPoint - 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                highlightPos.y += 144;
                highlight.GetComponent<RectTransform>().localPosition = highlightPos;
            }
        }

        if (highlightPos.y >= lowestPoint + 1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                highlightPos.y -= 144;
                highlight.GetComponent<RectTransform>().localPosition = highlightPos;
            }
        }
    }
}
