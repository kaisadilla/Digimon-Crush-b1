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
            HP.maxValue = fighter.GetMaxHP();
            HP.value = fighter.GetCurrentHP();
            energy.maxValue = fighter.GetMaxEnergy();
            energy.value = fighter.GetCurrentEnergy();
        }

        void Update() {
            HP.value = fighter.GetCurrentHP();
            energy.value = fighter.GetCurrentEnergy();
        }
    }
}