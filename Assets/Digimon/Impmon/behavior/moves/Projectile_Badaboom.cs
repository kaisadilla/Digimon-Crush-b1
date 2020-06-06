using Kaisa.DigimonCrush.Fighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile_Badaboom : Projectile {
        [SerializeField] protected Rigidbody2D body;
        protected override void OnStart() {
            initialX = transform.position.x;
            float speedX = goingLeft ? -move.Speed : move.Speed;
            body.velocity = new Vector2(speedX, -2);
        }

        protected override void Travel() { }
    }
}