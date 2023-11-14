﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateLevel : MonoBehaviour
{
    List<Room> generatedRooms = new List<Room>();
    bool itemroomfound = false;
    bool shopfound = false;
    bool bossroomfound = false;

    void DrawOnMap(Room R)
    {
        foreach (Room alreadyexistingroom in Level.RoomList)
        {
            if (alreadyexistingroom.position == R.position) return;
        }

        float roomImageSize = Level.MapSize / 10;

        GameObject Map = GameObject.Find("Map");
        GameObject Panel = new GameObject("MapTile");
        Panel.AddComponent<CanvasRenderer>();
        UnityEngine.UI.Image i = Panel.AddComponent<UnityEngine.UI.Image>();

        switch (R.RoomNumber)
        {
            case 0:
                {
                    i.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/Location.jpg", typeof(Sprite));
                    break;
                }
            case 1:
                {
                    i.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/Boss.png", typeof(Sprite));
                    break;
                }
            case 2:
                {
                    i.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/Shop.jpg", typeof(Sprite));
                    break;
                }
            case 3:
                {
                    i.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/TreasureRoom.jpg", typeof(Sprite));
                    break;
                }
            default:
                {
                    i.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Images/MapImages/EmptyRoom.jpg", typeof(Sprite));
                    break;
                }


        }

        R.RoomImage = i;

        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(roomImageSize, roomImageSize);
        Panel.transform.localPosition = new Vector3(roomImageSize * R.position.x, roomImageSize * R.position.y, 0);

        Panel.transform.SetParent(Map.transform, false);

        if (R.RoomNumber > 3)
        {
            R.RoomNumber = UnityEngine.Random.Range(6, GameObject.Find("Rooms").transform.childCount);
        }

        Level.RoomList.Add(R);
    }


    bool CheckIfRoomsAreTouching(Vector2 v, string direction)
    {
        bool touching = false;


        foreach (var room in Level.RoomList)
        {
            if (direction == "down")
            {
                if (v.x - 1 == room.position.x && v.y == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x == room.position.x && v.y - 1 == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x + 1 == room.position.x && v.y == room.position.y)
                {
                    touching = true;
                    break;
                }
            }
            if (direction == "up")
            {
                if (v.x - 1 == room.position.x && v.y == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x == room.position.x && v.y + 1 == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x + 1 == room.position.x && v.y == room.position.y)
                {
                    touching = true;
                    break;
                }
            }
            if (direction == "left")
            {
                if (v.x - 1 == room.position.x && v.y == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x == room.position.x && v.y - 1 == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x == room.position.x && v.y + 1 == room.position.y)
                {
                    touching = true;
                    break;
                }
            }
            if (direction == "right")
            {
                if (v.x + 1 == room.position.x && v.y == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x == room.position.x && v.y - 1 == room.position.y)
                {
                    touching = true;
                    break;
                }
                if (v.x == room.position.x && v.y + 1 == room.position.y)
                {
                    touching = true;
                    break;
                }
            }
        }


        return touching;
    }

    bool NoMoreRooms(Room R)
    {
        if (!R.Leftdoor && !R.Rightdoor && !R.Topdoor && !R.Bottomdoor) return true;

        return false;
    }

    void GenerateAttachedRooms(Room R)
    {
        bool dont = false;

        if (R.Leftdoor)
        {
            bool alreadyexists = false;

            Vector2 newRoomPosition = new Vector2(R.position.x - 1, R.position.y);

            foreach (Room existingroom in Level.RoomList)
            {
                if (existingroom.position.x == newRoomPosition.x && existingroom.position.y == newRoomPosition.y)
                {
                    alreadyexists = true;
                }
            }

            if (!alreadyexists && R.position.x > -Level.MaxRooms && (!CheckIfRoomsAreTouching(newRoomPosition, "left")))
            {
                Room LeftRoom = new Room();
                LeftRoom.Topdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                LeftRoom.Leftdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                LeftRoom.Bottomdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                LeftRoom.Rightdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                LeftRoom.RoomNumber = 6;
                LeftRoom.position = newRoomPosition;
                // Отладочные логи перед установкой дверей
                //UnityEngine.Debug.Log("Before GenerateAttachedRooms: Leftdoor = " + LeftRoom.Leftdoor + ", Topdoor = " + LeftRoom.Topdoor + ", Bottomdoor = " + LeftRoom.Bottomdoor + ", Rightdoor = " + LeftRoom.Rightdoor);


                // Отладочные логи перед вызовом NoMoreRooms
               // UnityEngine.Debug.Log("Before NoMoreRooms: Leftdoor = " + LeftRoom.Leftdoor + ", Topdoor = " + LeftRoom.Topdoor + ", Bottomdoor = " + LeftRoom.Bottomdoor + ", Rightdoor = " + LeftRoom.Rightdoor);
                if (NoMoreRooms(LeftRoom) && !itemroomfound && !dont)
                {
                    itemroomfound = true;
                    LeftRoom.RoomNumber = 3;
                    DrawOnMap(LeftRoom);
                    dont = true;
                    // Отладочные логи после вызова NoMoreRooms
                   // UnityEngine.Debug.Log("After NoMoreRooms: Leftdoor = " + LeftRoom.Leftdoor + ", Topdoor = " + LeftRoom.Topdoor + ", Bottomdoor = " + LeftRoom.Bottomdoor + ", Rightdoor = " + LeftRoom.Rightdoor);
                }
                if (NoMoreRooms(LeftRoom) && !shopfound && !dont)
                {
                    shopfound = true;
                    LeftRoom.RoomNumber = 2;
                    DrawOnMap(LeftRoom);
                    dont = true;
                }
                if (NoMoreRooms(LeftRoom) && !bossroomfound && !dont)
                {
                    bossroomfound = true;
                    LeftRoom.RoomNumber = 1;
                    DrawOnMap(LeftRoom);
                    dont = true;
                }
                else if (!dont)
                {
                    DrawOnMap(LeftRoom);
                    generatedRooms.Add(LeftRoom); // Добавляем комнату в список сгенерированных комнат
                    GenerateAttachedRooms(LeftRoom);
                }
                // Отладочные логи после установки дверей
               // UnityEngine.Debug.Log("After GenerateAttachedRooms: Leftdoor = " + LeftRoom.Leftdoor + ", Topdoor = " + LeftRoom.Topdoor + ", Bottomdoor = " + LeftRoom.Bottomdoor + ", Rightdoor = " + LeftRoom.Rightdoor);

            }

        }

        if (R.Rightdoor)
        {
            bool alreadyexists = false;

            Vector2 newRoomPosition = new Vector2(R.position.x + 1, R.position.y);

            foreach (Room existingroom in Level.RoomList)
            {
                if (existingroom.position.x == newRoomPosition.x && existingroom.position.y == newRoomPosition.y)
                {
                    alreadyexists = true;
                }
            }

            if (!alreadyexists && R.position.x < Level.MaxRooms && (!CheckIfRoomsAreTouching(newRoomPosition, "right")))
            {
                Room RightRoom = new Room();
                RightRoom.Topdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                RightRoom.Rightdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                RightRoom.Bottomdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                RightRoom.Leftdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                RightRoom.RoomNumber = 6;
                RightRoom.position = newRoomPosition;

                if (NoMoreRooms(RightRoom) && !itemroomfound && !dont)
                {
                    itemroomfound = true;
                    RightRoom.RoomNumber = 3;
                    DrawOnMap(RightRoom);
                    dont = true;

                }
                if (NoMoreRooms(RightRoom) && !shopfound && !dont)
                {
                    shopfound = true;
                    RightRoom.RoomNumber = 2;
                    DrawOnMap(RightRoom);
                    dont = true;
                }
                if (NoMoreRooms(RightRoom) && !bossroomfound && !dont)
                {
                    bossroomfound = true;
                    RightRoom.RoomNumber = 1;
                    DrawOnMap(RightRoom);
                    dont = true;
                }
                else if (!dont)
                {
                    DrawOnMap(RightRoom);
                    GenerateAttachedRooms(RightRoom);
                }

            }

        }
        if (R.Topdoor)
        {
            bool alreadyexists = false;

            Vector2 newRoomPosition = new Vector2(R.position.x, R.position.y + 1);

            foreach (Room existingroom in Level.RoomList)
            {
                if (existingroom.position.x == newRoomPosition.x && existingroom.position.y == newRoomPosition.y)
                {
                    alreadyexists = true;
                }
            }

            if (!alreadyexists && R.position.y < Level.MaxRooms && (!CheckIfRoomsAreTouching(newRoomPosition, "up")))
            {
                Room TopRoom = new Room();
                TopRoom.Topdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                TopRoom.Rightdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                TopRoom.Leftdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                TopRoom.Bottomdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                TopRoom.RoomNumber = 6;
                TopRoom.position = newRoomPosition;

                if (NoMoreRooms(TopRoom) && !itemroomfound && !dont)
                {
                    itemroomfound = true;
                    TopRoom.RoomNumber = 3;
                    DrawOnMap(TopRoom);
                    dont = true;

                }
                if (NoMoreRooms(TopRoom) && !shopfound && !dont)
                {
                    shopfound = true;
                    TopRoom.RoomNumber = 2;
                    DrawOnMap(TopRoom);
                    dont = true;
                }
                if (NoMoreRooms(TopRoom) && !bossroomfound && !dont)
                {
                    bossroomfound = true;
                    TopRoom.RoomNumber = 1;
                    DrawOnMap(TopRoom);
                    dont = true;
                }
                else if (!dont)
                {
                    DrawOnMap(TopRoom);
                    GenerateAttachedRooms(TopRoom);
                }

            }
        }
        if (R.Bottomdoor)
        {
            bool alreadyexists = false;

            Vector2 newRoomPosition = new Vector2(R.position.x, R.position.y - 1);

            foreach (Room existingroom in Level.RoomList)
            {
                if (existingroom.position.x == newRoomPosition.x && existingroom.position.y == newRoomPosition.y)
                {
                    alreadyexists = true;
                }
            }

            if (!alreadyexists && R.position.y > -Level.MaxRooms && (!CheckIfRoomsAreTouching(newRoomPosition, "down")))
            {
                Room BottomRoom = new Room();
                BottomRoom.Topdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                BottomRoom.Rightdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                BottomRoom.Leftdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                BottomRoom.Bottomdoor = UnityEngine.Random.value > Level.RoomGenerationChance;
                BottomRoom.RoomNumber = 6;
                BottomRoom.position = newRoomPosition;

                if (NoMoreRooms(BottomRoom) && !itemroomfound && !dont)
                {
                    itemroomfound = true;
                    BottomRoom.RoomNumber = 3;
                    DrawOnMap(BottomRoom);
                    dont = true;

                }
                if (NoMoreRooms(BottomRoom) && !shopfound && !dont)
                {
                    shopfound = true;
                    BottomRoom.RoomNumber = 2;
                    DrawOnMap(BottomRoom);
                    dont = true;
                }
                if (NoMoreRooms(BottomRoom) && !bossroomfound && !dont && R.RoomNumber != 0)
                {
                    bossroomfound = true;
                    BottomRoom.RoomNumber = 1;
                    DrawOnMap(BottomRoom);
                    dont = true;
                }
                else if (!dont)
                {
                    DrawOnMap(BottomRoom);
                    GenerateAttachedRooms(BottomRoom);
                }

            }
        }

    }


    private void Start()
    {
        int maxtries = 0;
        int maxMaxTries = 1000;

        Room startRoom = new Room();
        startRoom.Topdoor = true;
        startRoom.Leftdoor = true;
        startRoom.Rightdoor = true;
        startRoom.Bottomdoor = true;
        startRoom.Cleared = true;
        startRoom.RoomNumber = 0;

        DrawOnMap(startRoom);

        GenerateAttachedRooms(startRoom);

        if (!itemroomfound || !shopfound || !bossroomfound)
        {
            itemroomfound = false;
            shopfound = false;
            bossroomfound = false;

            generatedRooms.Clear(); // Очищаем список сгенерированных комнат

            Level.RoomList.Clear();

            GameObject Map = GameObject.Find("Map");
            foreach (Transform child in Map.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            maxtries += 1;
            if (maxtries < maxMaxTries)
            {
                Start(); // Повторяем генерацию
            }

        }
        else
        {
            Player.CurrentRoom = startRoom;
            ChangeRooms.DrawDoors(startRoom);



        }




    }

    public void Redraw()
    {

        float roomImageSize = Level.MapSize / 10;

        GameObject Map = GameObject.Find("Map");

        foreach (Room R in Level.RoomList)
        {
            GameObject Panel = new GameObject("MapTile");
            Panel.AddComponent<CanvasRenderer>();
            UnityEngine.UI.Image i = Panel.AddComponent<UnityEngine.UI.Image>();

            i.sprite = R.RoomImage.sprite;

            R.RoomImage = i;

            Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(roomImageSize, roomImageSize);
            Panel.transform.localPosition = new Vector3(roomImageSize * R.position.x, roomImageSize * R.position.y, 0);

            Panel.transform.SetParent(Map.transform, false);

        }


    }

    Vector2 startMousePosition = Vector2.zero;
    Vector2 oldmousepos = Vector2.zero;
    public Vector2 mousedif = Vector2.zero;

    public float currentScale = 0f;

    private void Update()
    {
        {
            if (Input.GetMouseButton(2))
            {
                Vector2 newmousepos = Input.mousePosition;
                if (oldmousepos != Vector2.zero)
                {
                    mousedif = newmousepos - oldmousepos;
                    startMousePosition = new Vector2(startMousePosition.x + mousedif.x, startMousePosition.y + mousedif.y);
                    GameObject MapCanvas = GameObject.Find("MapCanvas");
                    GameObject Map = MapCanvas.transform.Find("Map").gameObject;
                    foreach (Transform child in Map.transform)
                    {
                        child.transform.position = new Vector3(child.transform.position.x + mousedif.x, child.transform.position.y + mousedif.y, 0);

                    }

                }
                oldmousepos = newmousepos;
            }
            else
            {
                oldmousepos = Vector2.zero;
            }

        }

        //Scrollwheel resize
        {
            currentScale = Level.MapSize; //200

            Vector2 mousescroll = Input.mouseScrollDelta;
            Level.MapSize += (mousescroll.y * 3f);

            if (Level.MapSize > 1000f) Level.MapSize = 1000f;
            if (Level.MapSize < 1f) Level.MapSize = 1f;

            if (Level.MapSize != currentScale)
            {

                GameObject Map = GameObject.Find("Map");
                foreach (Transform child in Map.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                Redraw();
            }
        }
    }

}