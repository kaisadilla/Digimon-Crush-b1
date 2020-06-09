using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_Kohenkyo : Move {
        public Renamon_Kohenkyo(DigimonFighter user) : base(user) {
            AnimName = "attack_kohenkyo";
            InternalId = 0;
            Cooldown = 10f;
        }

        public override void OnFire() {
            GameObject enemy = user.GetOppositePlayer();

            Vector3 allyPos = user.transform.position;
            Vector3 enemyPos = enemy.transform.position;

            user.transform.position = enemyPos;
            enemy.transform.position = allyPos;
        }
    }
}