using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class InitializePlayer : MonoBehaviour
{
    
    [SerializeField] private Transform Position;
    [SerializeField] private Camera maincamera;
    [SerializeField] private Animator animator;

    void Start()
    {
        UnityEngine.Application.targetFrameRate = 300;

      
        Player.Position = Position;
        Player.PlayerCamera = maincamera;
        Player.animator = animator;
    }
}
