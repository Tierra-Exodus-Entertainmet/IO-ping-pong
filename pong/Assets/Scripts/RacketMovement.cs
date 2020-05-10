using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RacketMovement : NetworkBehaviour
{
    public float speed = 1500;
    public Rigidbody2D Rigidbody2d;


    void FixedUpdate()
    {
        // only let the local player control the racket.
        // don't control other player's rackets
        if (isLocalPlayer)
            Rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
    }

}
