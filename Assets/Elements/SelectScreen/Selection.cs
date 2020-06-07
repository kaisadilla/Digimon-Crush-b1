using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaisa.DigimonCrush.Misc {
    public static class Selection {
        private static int player0 = -1, player1 = -1;

        public static void SetPlayer0(int val) {
            player0 = val;
            if (player0 != -1 && player1 != -1) {
                SceneManager.LoadScene("SampleScene");
            }
        }

        public static void SetPlayer1(int val) {
            player1 = val;
            if (player0 != -1 && player1 != -1) {
                SceneManager.LoadScene("SampleScene");
            }
        }

        public static int GetPlayer0() => player0;
        public static int GetPlayer1() => player1;
    }
}