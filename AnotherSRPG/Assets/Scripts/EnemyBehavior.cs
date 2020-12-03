using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    GameMaster gm;
    public Unit currentEnemy;

    public float targetX;
    public float targetY;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();

        Vector2 position = new Vector2(9, 0);
        currentEnemy.EnemyMove(position);
    }

    private void Update()
    {
        if (gm.allyTurn == false)
        {
            Behavior();
        }
    }

    void Behavior()
    {
           foreach(Unit unit in FindObjectsOfType<Unit>())
            {
                if(unit == unit.ally)
                {
                    targetX = unit.transform.position.x;
                    targetY = unit.transform.position.y;

                    foreach (Tile tile in FindObjectsOfType<Tile>())
                    {
                        if(Mathf.Abs(currentEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - tile.transform.position.y) + tile.cost == currentEnemy.stat.movement)
                        {
                            if(tile.transform.position.x == targetX)
                            {
                                if (tile.IsClear() == true)
                                {
                                    currentEnemy.Move(tile.transform.position);
                                }
                            }
                        }
                    }
                }
            }
    }
}
