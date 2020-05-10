using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallMovement : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody2D rigidbody2d;

    public override void OnStartServer()
    {
        base.OnStartServer();

        // only simulate ball physics on server
        rigidbody2d.simulated = true;

        // Serve the ball from left player
        rigidbody2d.velocity = Vector2.right * speed;
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    [ServerCallback]
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Calculate y direction via hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate x direction via opposite collision
            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(x, y).normalized;

            // Set Velocity with dir * speed
            rigidbody2d.velocity = dir * speed;
        }
    }
}
