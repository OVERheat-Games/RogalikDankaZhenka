using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float attackSpeed = 2.5f;
    public static float RunSpeed = 80f;
    public static float AttackDamage = 1f;
    public static float ShotSpeed = 70f;

    public static Transform StaffPosition;
    public static Animator animator;
    public static Camera PlayerCamera;
    public static CharacterController PlayerController;

    public static Room CurrentRoom;
}
