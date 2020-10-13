using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public bool selected;
    GameMaster gm;
    public Stats stat;

    public Weapon equippedWeapon;

    public bool ally;

    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;

    public bool hasMoved;

    public float moveSpeed;

    public GameObject attackableIcon;
    BattleManager bm;

    public GameObject cursorObject;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        bm = FindObjectOfType<BattleManager>();
        cursorObject = GameObject.FindGameObjectWithTag("Cursor");
        stat = GetComponent<Stats>();

        //Sets your combat stats 
        stat.attack = stat.strength + equippedWeapon.might;
        stat.hit = equippedWeapon.hit + ((stat.skill * 3) / 2);
        stat.crit = equippedWeapon.crit + (stat.skill / 2);
        stat.avoid = (stat.speed * 3) / 2;
    }

    private void Update()
    {
        if(cursorObject.transform.position == transform.position)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                CheckStats();
            }
        }
        if (cursorObject.transform.position == transform.position)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Selected();
            }
        }
    }

    private void CheckStats()
    {
        gm.ToggleStatsPanel(this);
    }

    private void Selected()
    {
        ResetAttackableIcons();

        if(selected == true)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else
        {
            if(ally == gm.allyTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }

                selected = true;
                gm.selectedUnit = this;

                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(cursorObject.transform.position, 0.15f);
        Unit unit = col.GetComponent<Unit>();
        if(gm.selectedUnit != null)
        {
            if(gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                if(cursorObject.transform.position == unit.transform.position)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        gm.selectedUnit.bm.Combat(gm.selectedUnit, unit);
                    }
                }
            }
        }
    }

    void GetWalkableTiles()
    {
        if(hasMoved == true)
        {
            return;
        }
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= stat.movement)
            {
                if(tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }

    void GetEnemies()
    {
        enemiesInRange.Clear();

        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= equippedWeapon.range)
            {
                if(unit.ally != gm.allyTurn && hasAttacked == false)
                {
                    enemiesInRange.Add(unit);
                    unit.attackableIcon.SetActive(true);
                }
            }
        }
    }

    public void ResetAttackableIcons()
    {
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.attackableIcon.SetActive(false);
        }
    }

    public void Move(Vector2 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        while(transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;

        }

        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
            yield return null;

        }

        hasMoved = true;
        ResetAttackableIcons();
        GetEnemies();
        gm.MoveStatsPanel(this);
    }

    private void LevelUp()
    {
        if(stat.experience >= 100)
        {
            stat.level += 1;
            stat.experience = 0;
        }
    }

    private void GainExperience(int experience)
    {
        stat.experience += experience;
    }
}
