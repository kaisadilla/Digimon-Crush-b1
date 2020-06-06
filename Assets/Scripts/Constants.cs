using UnityEngine;

public static class Constants {
    //public const int layerPlatform = 10;
    public const float DEFAULT_IMMUNITY = 1f;
    public const int layerPlayer = 11;
    public const int layerGhostedPlayer = 12; //A layer for characters that are ghosted (do not collide with other characters).

    public static readonly Color defaultColor = Color.white;
    public static readonly Color slowColor = Color.cyan;
}

public enum PlayerState {
    Idle = 0,
    Walking = 1,
    Running = 2,
    Jumping = 3,
    Falling = 4,
    Guard = 5,
    Hit = 6,
    KnockedBack = 7,
    Taunting = 10
}