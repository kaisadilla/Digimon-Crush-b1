using Kaisa.DigimonCrush.Fighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaisa.DigimonCrush {
    public class HealthBar : MonoBehaviour {
        [SerializeField] private int player;
        private Fighter.DigimonFighter fighter;
        [SerializeField] private Slider HP;
        [SerializeField] private Slider energy;
        [SerializeField] private Slider power;
        [SerializeField] private Image powerFillBar;
        void Start() {
            GetFighter();
            HP.maxValue = fighter.MaxHP;
            HP.value = fighter.CurrentHP;
            energy.maxValue = fighter.MaxEnergy;
            energy.value = fighter.CurrentEnergy;
            power.maxValue = 15;
            power.value = 0;
        }

        void Update() {
            HP.value = fighter.CurrentHP;
            energy.value = fighter.CurrentEnergy;
            power.value = fighter.CurrentPower;

            if (!fighter.Enhanced) {
                if(power.value <= 8) {
                    powerFillBar.color = new Color(255f / 255f, 255 / 255f, 105 / 255f);
                }
                else if (power.value <= 12) {
                    powerFillBar.color = new Color(255 / 255f, 180 / 255f, 69 / 255f);
                }
                else {
                    powerFillBar.color = new Color(255 / 255f, 100 / 255f, 29 / 255f);
                }
            }
            else {
                powerFillBar.color = new Color(255 / 255f, 42 / 255f, 0 / 255f);
            }
        }

        private void GetFighter() {
            if (player == 0) {
                fighter = GameManager.Instance.player0.GetComponent<DigimonFighter>();
            }
            else if (player == 1) {
                fighter = GameManager.Instance.player1.GetComponent<DigimonFighter>();
            }
        }
    }
}