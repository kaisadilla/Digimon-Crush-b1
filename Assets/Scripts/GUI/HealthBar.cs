using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaisa.DigimonCrush {
    public class HealthBar : MonoBehaviour {
        [SerializeField] private Fighter.DigimonFighter fighter;
        [SerializeField] private Slider HP;
        [SerializeField] private Slider energy;
        void Start() {
            HP.maxValue = fighter.MaxHP;
            HP.value = fighter.CurrentHP;
            energy.maxValue = fighter.MaxEnergy;
            energy.value = fighter.CurrentEnergy;
        }

        void Update() {
            HP.value = fighter.CurrentHP;
            energy.value = fighter.CurrentEnergy;
        }
    }
}