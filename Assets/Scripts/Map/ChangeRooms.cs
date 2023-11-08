using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Plane;
    [SerializeField] private GameObject Rooms;

    int currentX = 0;
    int currentY = 0;

    Sprite previousSprite;

    private void Start()
    {
        previousSprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/EmptyRoom.jpg", typeof(Sprite));
    }

    public void ChangeRoom()
    {
        // UnityEngine.Debug.Log("Before ChangeRoom: currentX = " + currentX + ", currentY = " + currentY);
        foreach (Room R in Level.RoomList)
        {
            if (currentX == R.position.x && currentY == R.position.y)
            {
                Player.CurrentRoom.RoomImage.sprite = previousSprite;
                Player.CurrentRoom = R;
                DrawDoors(R);
                previousSprite = Player.CurrentRoom.RoomImage.sprite;
                Player.CurrentRoom.RoomImage.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/Location.jpg", typeof(Sprite));

                
                foreach (Transform child in GameObject.Find("Rooms").transform)
                {
                    child.gameObject.SetActive(false);
                }
                


                GameObject.Find("Rooms").transform.Find(R.RoomNumber.ToString()).gameObject.SetActive(true);
                // Здесь вы можете также обновить состояние дверей в текущей комнате, если требуется.
            }
            continue;
        }
        // UnityEngine.Debug.Log("After ChangeRoom: currentX = " + currentX + ", currentY = " + currentY);
    }

    public static void DrawDoors(Room R)
    {
        GameObject Doors = GameObject.Find("Doors");

        for (int i = 0; i < Doors.transform.childCount; i++)
        {
            Doors.transform.GetChild(i).gameObject.SetActive(false);
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
                Doors.transform.Find("TopDoor").gameObject.SetActive(true);
            }
            if (room.position.x == R.position.x && room.position.y == R.position.y - 1)
            {
                Doors.transform.Find("BottomDoor").gameObject.SetActive(true);
            }
            if (room.position.x == R.position.x - 1 && room.position.y == R.position.y)
            {
                Doors.transform.Find("LeftDoor").gameObject.SetActive(true);
            }
            if (room.position.x == R.position.x + 1 && room.position.y == R.position.y)
            {
                Doors.transform.Find("RightDoor").gameObject.SetActive(true);
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
                    newPosition = new Vector3(6, -4, 0);
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
