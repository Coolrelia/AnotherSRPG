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

    public GameObject menuPanel;
    public GameObject statsPanel;
    public Vector2 statsPanelShift;
    public Unit viewedUnit;

    public Text nameText;
    public Text healthText;
    public Text attackText;
    public Text hitText;
    public Text critText;
    public Text avoidText;
    public Text expText;
    public Text levelText;

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
        if(allyTurn == true)
        {
            if(Input.GetKeyDown(KeyCode.M) && menuPanel.activeSelf == false)
            {
                menuPanel.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.M) && menuPanel.activeSelf == true)
            {
                menuPanel.SetActive(false);
            }
        }

        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if(unit.stat.health <= 0)
            {
                if (unit.ally == true)
                {
                    alliedUnits.Remove(unit);
                    Destroy(unit.gameObject);
                }
                else
                {
                    enemyUnits.Remove(unit);
                    Destroy(unit.gameObject);
                }
            }
        }

        turnNumberText.text = turnNumber.ToString();

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
            expText.text = viewedUnit.stat.experience.ToString() + "/100";
            levelText.text = viewedUnit.stat.level.ToString();
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
            menuPanel.SetActive(false);
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
