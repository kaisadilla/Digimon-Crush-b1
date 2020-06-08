using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmon_PetitDejeuner : Move {
        private int type;
        public BlackGuilmon_PetitDejeuner(DigimonFighter user) : base(user) {
            type = Random.Range(1, 5);
            AnimName = $"attack_petitDejeuner{type}";
        }

        public override void OnFire() {
            switch (type) {
                case 1:
                case 2:
                    (user.Movement as BlackGuilmonMovement)?.Empower();
                    user.Audio.PlaySound("boost");
                    break;
                case 3:
                    user.Audio.PlayHit();
                    user.AddHP(-5f);
                    break;
                case 4:
                    user.Audio.PlaySound("swallow");
                    for (int i = 0; i < Random.Range(3, 12); i++) {
                        user.SpawnPoint(user.transform.position, false, Random.Range(0, 2) == 0);
                    }
                    break;
            }
        }
    }
}