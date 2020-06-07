using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Guilmon_PetitDejeuner : Move {
        private int type;
        public Guilmon_PetitDejeuner(DigimonFighter user) : base(user) {
            type = Random.Range(1, 6);
            AnimName = $"attack_petitDejeuner{type}";
        }

        public override void OnFire() {
            switch (type) {
                case 1:
                case 2:
                case 3:
                    user.Audio.PlayGather();
                    user.AddHP(5f);
                    user.AddPower(1);
                    break;
                case 4:
                    user.Audio.PlayHit();
                    user.AddHP(-5f);
                    user.AddPower(2);
                    break;
                case 5:
                    user.ApplyBurn(2f);
                    break;
            }
        }
    }
}