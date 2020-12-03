using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public GameObject cursorObject;
    public float hoverAmount;

    private void Start()
    {
        cursorObject = GameObject.FindGameObjectWithTag("Cursor");
    }

    public void Update()
    {
        CursorEnter();
        CursorExit();
    }

    private void CursorEnter()
    {
        if(cursorObject.transform.position == transform.position)
        {
            transform.localScale += Vector3.one * hoverAmount;
        }
    }
    private void CursorExit()
    {
        if (cursorObject.transform.position == transform.position)
        {
            transform.localScale -= Vector3.one * hoverAmount;
        }
    }
}
