using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public bool allyTurn = true;
    public int turnNumber;

    public GameObject selectedUnitSquare;

    public GameObject statsPanel;
    public Vector2 statsPanelShift;
    public Unit viewedUnit;

    public Text nameText;
    public Text healthText;
    public Text attackText;
    public Text hitText;
    public Text critText;
    public Text avoidText;

    public Text turnNumberText;

    public GameObject youngLeah;
    public GameObject ophelia;

    public List<Unit> alliedUnits = new List<Unit>();
    public List<Unit> enemyUnits = new List<Unit>();

    private void Start()
    {
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if(unit.ally == true)
            {
                alliedUnits.Add(unit);
            }
            else
            {
                enemyUnits.Add(unit);
            }
        }
    }

    private void Update()
    {
        turnNumberText.text = turnNumber.ToString();

        if (turnNumber == 2)
        {
            youngLeah.SetActive(true);
        }

        if(enemyUnits.Count <= 0)
        {
            ophelia.SetActive(true);
        }

        if (selectedUnit != null)
        {
            selectedUnitSquare.SetActive(true);
            selectedUnitSquare.transform.position = selectedUnit.transform.position;
        }
        else
        {
            selectedUnitSquare.SetActive(false);
        }
    }

    public void ResetTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    public void ToggleStatsPanel(Unit unit)
    {
        if(unit.Equals(viewedUnit) == false)
        {
            statsPanel.SetActive(true);
            statsPanel.transform.position = (Vector2)unit.transform.position + statsPanelShift;
            viewedUnit = unit;
            UpdateStatsPanel();
        }
        else
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }

    public void UpdateStatsPanel()
    {
        if(viewedUnit != null)
        {
            nameText.text = viewedUnit.gameObject.name.ToString();
            healthText.text = "HP " + viewedUnit.stat.health.ToString();
            attackText.text = "Atk " + viewedUnit.stat.attack.ToString();
            hitText.text = "Hit " + viewedUnit.stat.hit.ToString();
            critText.text = "Crit " + viewedUnit.stat.crit.ToString();
            avoidText.text = "Avo " + viewedUnit.stat.avoid.ToString();
        }
    }

    public void MoveStatsPanel(Unit unit)
    {
        if(unit.Equals(viewedUnit))
        {
            statsPanel.transform.position = (Vector2)unit.transform.position + statsPanelShift;
        }
    }

    public void RemoveStatsPanel(Unit unit)
    {
        if(unit.Equals(viewedUnit))
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }

    public void EndTurn()
    {
        if(allyTurn == true)
        {
            allyTurn = false;
        }
        else if(allyTurn == false)
        {
            allyTurn = true;
            turnNumber++;
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
