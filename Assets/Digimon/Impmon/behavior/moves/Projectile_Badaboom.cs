using Kaisa.DigimonCrush.Fighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile_Badaboom : Projectile {
        [SerializeField] protected Rigidbody2D body;
        protected override void OnStart() {
            initialX = transform.position.x;
            float speedX = goingLeft ? -move.Speed.x : move.Speed.x;
            body.velocity = new Vector2(speedX, move.Speed.y);
        }

        protected override void Travel() { }
    }
}