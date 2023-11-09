using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class Zombie : Enemy
{
   public override void DoSothing()
    {
        if(collision.collider.name == "Player" && !Player.Invincible)
        {
            Player.Health -= .5f;

            if(Player.Health > 0)
            {
                Player.Invincible = true;
            }

        }
    }
}
