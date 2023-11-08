using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        UnityEngine.Debug.Log("Before ChangeRoom: currentX = " + currentX + ", currentY = " + currentY);
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
        UnityEngine.Debug.Log("After ChangeRoom: currentX = " + currentX + ", currentY = " + currentY);
    }

    public static void DrawDoors(Room R)
    {
        GameObject Doors = GameObject.Find("Doors");

        for (int i = 0; i < Doors.transform.childCount; i++)
        {
            Doors.transform.GetChild(i).gameObject.SetActive(true);
        }

        // Добавим флаги для каждой двери
        bool topDoor = false;
        bool bottomDoor = false;
        bool rightDoor = false;
        bool leftDoor = false;

        // Пройдемся по всем соседним комнатам
        foreach (Room room in Level.RoomList)
        {
            if (room.position.x == R.position.x && room.position.y == R.position.y + 1)
            {
                topDoor = !room.Bottomdoor;
            }
            if (room.position.x == R.position.x && room.position.y == R.position.y - 1)
            {
                bottomDoor = !room.Topdoor;
            }
            if (room.position.x == R.position.x + 1 && room.position.y == R.position.y)
            {
                rightDoor = !room.Leftdoor;
            }
            if (room.position.x == R.position.x - 1 && room.position.y == R.position.y)
            {
                leftDoor = !room.Rightdoor;
            }
        }

        // Устанавливаем активность дверей на основе флагов
        Doors.transform.Find("TopDoor").gameObject.SetActive(topDoor);
        Doors.transform.Find("BottomDoor").gameObject.SetActive(bottomDoor);
        Doors.transform.Find("RightDoor").gameObject.SetActive(rightDoor);
        Doors.transform.Find("LeftDoor").gameObject.SetActive(leftDoor);
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
