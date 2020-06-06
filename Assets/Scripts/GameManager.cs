using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush {
    public class GameManager : MonoBehaviour {
        public GameObject player0, player1;

        public static GameManager Instance { get; private set; }

        public void Awake() {
            Instance = this;
        }
    }
}