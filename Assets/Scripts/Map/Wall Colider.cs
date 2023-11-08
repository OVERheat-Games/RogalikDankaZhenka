using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public GameObject TopWithDoor;
    public GameObject TopWithoutDoor;
    public GameObject BottomWithDoor;
    public GameObject BottomWithoutDoor;
    public GameObject LeftWithDoor;
    public GameObject LeftWithoutDoor;
    public GameObject RightWithDoor;
    public GameObject RightWithoutDoor;

    void Update()
    {
        if (GameObject.Find("TopDoor"))
        {
            TopWithDoor.SetActive(true);
            TopWithoutDoor.SetActive(false);
        }
        else
        {
            TopWithDoor.SetActive(false);
            TopWithoutDoor.SetActive(true);
        }

        if (GameObject.Find("BottomDoor"))
        {
            BottomWithDoor.SetActive(true);
            BottomWithoutDoor.SetActive(false);
        }
        else
        {
            BottomWithDoor.SetActive(false);
            BottomWithoutDoor.SetActive(true);
        }

        if (GameObject.Find("LeftDoor"))
        {
            LeftWithDoor.SetActive(true);
            LeftWithoutDoor.SetActive(false);
        }
        else
        {
            LeftWithDoor.SetActive(false);
            LeftWithoutDoor.SetActive(true);
        }

        if (GameObject.Find("RightDoor"))
        {
            RightWithDoor.SetActive(true);
            RightWithoutDoor.SetActive(false);
        }
        else
        {
            RightWithDoor.SetActive(false);
            RightWithoutDoor.SetActive(true);
        }
    }
}

