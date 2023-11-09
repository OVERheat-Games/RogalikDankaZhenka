using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Enimy : MonoBehaviour
{
    public float MaxHealth = 3;
    public float CurrentHealt = 3;

    public bool EnemyAlreadyHit = false;

    private void Start()
    {
        CurrentHealt = MaxHealth;
    }

    public virtual void LimitVelocity()
    {
        Rigidbody rb = transform.GetComponent<Rigidbody2D>();
        float speed = Vector3.Magnitude(rb.velocity);

        if(speed > Level.MaximumVelocity)
        {
            float brakeSpeed = speed - Level.MaximumVelocity;
            Vector3 normalizedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalizedVelocity * brakeSpeed;

            rb.AddForce(-brakeVelocity);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {



    }
}

