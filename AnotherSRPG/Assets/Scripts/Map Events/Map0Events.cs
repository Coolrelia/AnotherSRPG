using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map0Events : MonoBehaviour
{
    GameMaster gm;

    public GameObject youngLeah;
    public GameObject ophelia;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        if (gm.turnNumber == 2)
        {
            youngLeah.SetActive(true);
        }

        if (gm.enemyUnits.Count <= 0)
        {
            ophelia.SetActive(true);
        }
    }
}
