﻿using Kaisa.DigimonCrush;
using Kaisa.DigimonCrush.Fighter;
using Kaisa.DigimonCrush.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChooser : MonoBehaviour {
    public GameObject[] digimon;
    public Transform position0, position1;
    public HealthBar healthBar0, healthBar1;

    void Awake() {
        if (Selection.GetPlayer0() == -1 || Selection.GetPlayer1() == -1) {
            return;
        }

        //var digimon0 = digimon[Random.Range(0, digimon.Length)];
        var digimon0 = digimon[Selection.GetPlayer0()];
        var player0 = Instantiate(digimon0, position0.position, Quaternion.Euler(0, 0, 0));
        var fighter0 = player0.GetComponent<DigimonFighter>();
        fighter0.Player = 0;
        //healthBar0.fighter = fighter0;
        GameManager.Instance.player0 = player0;
        //var digimon1 = digimon[Random.Range(0, digimon.Length)];
        var digimon1 = digimon[Selection.GetPlayer1()];
        var player1 = Instantiate(digimon1, position1.position, Quaternion.Euler(0, 0, 0));
        var fighter1 = player1.GetComponent<DigimonFighter>();
        fighter1.Player = 1;
        //healthBar1.fighter = fighter1;
        GameManager.Instance.player1 = player1;
    }
}
