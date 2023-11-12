using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public static bool Invincible = false;
    public static bool PauseMovement = false;

    public static float MaxHealth = 3f;
    public static float Health = 3f;
    public static float HeartLeftOffset = 20f;
    public static float HeartXSpacing = 28f;
    public static float HeartYSpacing = 45f;

    public static int Bombs = 99;
    public static int Keys = 99;
    public static int Crystals = 0;

    public static float AttackSpeed = 2f;
    public static float RunSpeed = 240f;
    public static float AttackDamage = 1f;
    public static float TemporaryAttackDamage = 0f;
    public static float ShotSpeed = 70f;

    public static float EnemyKnockBack = 25f;
    public static float ObjectKnockBack = 300f;

    public static float JumpDistance = 450f;
    public static bool JumpOnCooldown = false;
    public static float JumpCooldown = 2f;

    public static bool HoldingBomb = false;
    public static int BombFuse = 4;

    public static List<GameObject> FloorLoot = new List<GameObject>();

    public static GameObject Staff;
    public static Transform StaffPosition;
    public static Transform Position;
    public static Animator animator;
    public static Camera PlayerCamera;
    public static CharacterController PlayerController;

    public static List<Item> ExistingItems = new List<Item>();
    public static List<Item> Items = new List<Item>();

    public static bool PickingUpCard = false;
    public static bool CardOnCooldown = false;
    public static bool AlreadySwapping = false;
    public static string CurrentCard = "";

    public static Room CurrentRoom;

    public static bool ItemRoomAlreadyTaken = false;

    public static float GooPowerDuration = 60f;

    public static AudioSource Music;
}
