namespace Kaisa.DigimonCrush.Fighter {
    public class GuilmonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Guilmon_RockBreaker(fighter);
        // horizontal
        public override Move OnAttack1() => new Guilmon_RockBreaker(fighter);
        // up
        public override Move OnAttack2() => new Guilmon_PetitDejeuner(fighter);
        // down
        public override Move OnAttack3() => new Guilmon_PyroSphere(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Guilmon_SharpClaw(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Guilmon_SharpClaw(fighter);
        // jump + up
        public override Move OnAttack6() => new Guilmon_FireMitt(fighter);
        // jump + down
        public override Move OnAttack7() => new Guilmon_PyroSphere_Flying(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Guilmon_KurenaiMaru(fighter);
    }
}