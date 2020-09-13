using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;

    public float hoverAmount;

    public LayerMask obstacleLayer;

    public Sprite tile;
    public Sprite highlightedTile;

    public bool isWalkable;
    GameMaster gm;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameMaster>();
    }

    public bool IsClear()
    {
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        if(obstacle != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Highlight()
    {
        rend.sprite = highlightedTile;
        isWalkable = true;
    }

    public void Reset()
    {
        rend.sprite = tile;
        isWalkable = false;
    }

    void OnMouseDown()
    {
        if(isWalkable == true && gm.selectedUnit != null)
        {
            gm.selectedUnit.Move(this.transform.position);
        }
    }
}
