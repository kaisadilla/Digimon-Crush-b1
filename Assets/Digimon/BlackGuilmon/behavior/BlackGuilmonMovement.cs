namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmonMovement : DigimonMovement {
        //empower: 0, 1, 4, 5, 6, 8
        private bool empowered = false;

        public void Empower() {
            empowered = true;
        }
        // regular
        public override Move OnAttack0() => new Guilmon_RockBreaker(fighter);
        public Move OnAttack0Empowered() => new BlackGuilmon_RockBreaker_Empowered(fighter);
        // horizontal
        public override Move OnAttack1() => new Guilmon_RockBreaker(fighter);
        public Move OnAttack1Empowered() => new BlackGuilmon_RockBreaker_Empowered(fighter);
        // up
        public override Move OnAttack2() => new BlackGuilmon_PetitDejeuner(fighter);
        // down
        public override Move OnAttack3() => new Guilmon_PyroSphere(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Guilmon_SharpClaw(fighter);
        public Move OnAttack4Empowered() => new BlackGuilmon_SharpClaw_Empowered(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Guilmon_SharpClaw(fighter);
        public Move OnAttack5Empowered() => new BlackGuilmon_SharpClaw_Empowered(fighter);
        // jump + up
        public override Move OnAttack6() => new BlackGuilmon_WildUppercut(fighter);
        public Move OnAttack6Empowered() => new BlackGuilmon_WildUppercut_Empowered(fighter);
        // jump + down
        public override Move OnAttack7() => new Guilmon_PyroSphere_Flying(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Guilmon_KurenaiMaru(fighter);
        public Move OnAttack8Empowered() => new BlackGuilmon_KurenaiMaru_Empowered(fighter);

        public override void UseAttack0() {
            Move m;
            if (!empowered) {
                m = OnAttack0();
            }
            else {
                empowered = false;
                m = OnAttack0Empowered();
            }
            AssignMove(m);
        }
        public override void UseAttack1() {
            Move m;
            if (!empowered) {
                m = OnAttack1();
            }
            else {
                m = OnAttack1Empowered();
            }
            AssignMove(m);
        }
        public override void UseAttack2() {
            Move m = OnAttack2();
            AssignMove(m);
        }
        public override void UseAttack3() {
            Move m = OnAttack3();
            AssignMove(m);
        }
        public override void UseAttack4() {
            Move m;
            if (!empowered) {
                m = OnAttack4();
            }
            else {
                empowered = false;
                m = OnAttack4Empowered();
            }
            AssignMove(m);
        }
        public override void UseAttack5() {
            Move m;
            if (!empowered) {
                m = OnAttack5();
            }
            else {
                empowered = false;
                m = OnAttack5Empowered();
            }
            AssignMove(m);
        }
        public override void UseAttack6() {
            if (AirJumpAllowed) {
                AirJumpAllowed = false;
                Move m;
                if (!empowered) {
                    m = OnAttack6();
                }
                else {
                    empowered = false;
                    m = OnAttack6Empowered();
                }
                AssignMove(m);
            }
        }
        public override void UseAttack7() {
            Move m = OnAttack7();
            AssignMove(m);
        }
        public override void UseAttack8() {
            Move m;
            if (!empowered) {
                m = OnAttack8();
            }
            else {
                empowered = false;
                m = OnAttack8Empowered();
            }
            AssignMove(m);
        }
    }
}