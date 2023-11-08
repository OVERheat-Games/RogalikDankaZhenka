using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room
{
    public Vector2 position = Vector2.zero;

    public int RoomNumber = 0;

    public bool Topdoor = false;
    public bool Bottomdoor = false;
    public bool Leftdoor = false;
    public bool Rightdoor = false;
    


    public bool Cleared = false;
    public int EnemiesLeft = 0;

    public Image RoomImage;
}


public static class Level
{
    public static float RoomGenerationChance = .5f;
    public static float MapSize = 200f;
    public static float MaxRooms = 2;
    public static List<Room> RoomList = new List<Room>();

}