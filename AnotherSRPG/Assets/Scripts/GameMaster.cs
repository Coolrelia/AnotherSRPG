using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public bool allyTurn = true;

    public GameObject selectedUnitSquare;

    public void ResetTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }

        if(selectedUnit != null)
        {
            selectedUnitSquare.SetActive(true);
            selectedUnitSquare.transform.position = selectedUnit.transform.position;
        }
        else
        {
            selectedUnitSquare.SetActive(false);
        }
    }

    void EndTurn()
    {
        if(allyTurn == true)
        {
            allyTurn = false;
        }
        else if(allyTurn == false)
        {
            allyTurn = true;
        }

        if(selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.attackableIcon.SetActive(false);
            unit.hasAttacked = false;
        }
    }
}
