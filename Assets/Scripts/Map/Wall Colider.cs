using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColider : MonoBehaviour
{
    public GameObject WithDoor;
    public GameObject WithoutDoor;

    void Update()
    {
       if(GameObject.Find("TopDoor"))
       {
            WithDoor.SetActive(true); 
            WithoutDoor.SetActive(false);
       }
       else
       {
            WithDoor.SetActive(false); 
            WithoutDoor.SetActive(true);
       }
    }
}
