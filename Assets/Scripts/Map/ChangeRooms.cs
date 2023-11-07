using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{



    int currentX = 0;
    int currentY = 0;

    Sprite previousSprite;

    private void Start()
    {
        previousSprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/EmptyRoom.jpg", typeof(Sprite));
    }

    public void ChangeRoom()
    {
        foreach (Room R in Level.RoomList)
        {
            if (currentX == R.position.x && currentY == R.position.y)
            {
                Player.CurrentRoom.RoomImage.sprite = previousSprite;
                Player.CurrentRoom = R;
                DrawDoors(R);
                previousSprite = Player.CurrentRoom.RoomImage.sprite;
                Player.CurrentRoom.RoomImage.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/Location.jpg", typeof(Sprite));

                // Здесь вы можете также обновить состояние дверей в текущей комнате, если требуется.
            }
        }
    }

    public static void DrawDoors(Room R)
    {
        GameObject Doors = GameObject.Find("Doors");

        for (int i = 0; i < Doors.transform.childCount; i++)
        {
            Doors.transform.GetChild(i).gameObject.SetActive(true);
        }

        foreach (Room room in Level.RoomList)
        {
            if (room.position.x == R.position.x && room.position.y == R.position.y + 1)
            {
                Doors.transform.Find("TopDoor").gameObject.SetActive(!room.Bottomdoor);
            }
            if (room.position.x == R.position.x && room.position.y == R.position.y )
            {
                Doors.transform.Find("BottomDoor").gameObject.SetActive(!room.Topdoor);
            }
            if (room.position.x == R.position.x  && room.position.y == R.position.y)
            {
                Doors.transform.Find("RightDoor").gameObject.SetActive(!room.Leftdoor);
            }
            if (room.position.x == R.position.x  && room.position.y == R.position.y)
            {
                Doors.transform.Find("LeftDoor").gameObject.SetActive(!room.Rightdoor);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Vector3 newPosition = Vector3.zero;

            switch (other.gameObject.name)
            {
                case "TopDoor":
                    currentY++;
                    newPosition = new Vector3(6, 5, 0);
                    break;
                case "BottomDoor":
                    currentY--;
                    newPosition = new Vector3(5, 30, 0);
                    break;
                case "LeftDoor":
                    currentX--;
                    newPosition = new Vector3(32, 15, 0);
                    break;
                case "RightDoor":
                    currentX++;
                    newPosition = new Vector3(-28, 15, 0);
                    break;
            }

            transform.position = newPosition;
            ChangeRoom();
        }
        // Другие обработки столкновений
    }
}

