using Kaisa.DigimonCrush.Fighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimCaller : MonoBehaviour {
    [SerializeField] private DigimonFighter fighter;

    public void PlaySound(string sound) {
        fighter.Audio.PlaySound(sound);
    }

    public void Fire() {
        fighter.Movement.CurrentMove_Fire();
    }

    public void CallEffect(string effect) {
        fighter.Movement.CurrentMove_CallEffect(effect);
    }

    public void IncreaseHitCount() {
        fighter.Movement.CurrentMove_IncreaseCount();
    }

    public void EndCurrentMove() {
        fighter.Movement.EndCurrentMove();
    }

    public void EndCurrentMoveAfterTime(float delay) {
        fighter.Movement.EndCurrentMove(delay);
    }
}
