using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool Invincible = false;
    public static bool PauseMovement = false;
    public static bool Death = false;

    public static float MaxHealth = 3f;
    private static float health = 0f;
    public static float HeartLeftOffet = 20f;
    public static float HeartXSpacing = 28f;
    public static float HeartYSpacing = 45f;

    public static int Bombs = 1;
    public static int Keys = 1;

    public static float attackSpeed = 2.5f;
    public static float RunSpeed = 80f;
    public static float AttackDamage = 1f;
    public static float ShotSpeed = 70f;

    public static Transform StaffPosition;
    public static Animator animator;
    public static Camera PlayerCamera;
    public static CharacterController PlayerController;

    public static Room CurrentRoom;

    public static float Health
    {
        get{return health; }
        set
        {
            health = value;

            if (health <= 0)
            {
                health = 0;
                Death = true;
            }
        }
    }

    public static void Update()
    {
        if (Death)
        {
            PauseMovement = true;
            Debug.Log("Смерть.");
        }
        if(PauseMovement)
        {
            Time.timeScale = 0f;
        }
    }
    
     
}
