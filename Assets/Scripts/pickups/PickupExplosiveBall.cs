using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupExplosiveBall : MonoBehaviour
{
    private void ApplyEffect()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            ball.ActivateExplosion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pad"))
        {
            //применить эффект
            ApplyEffect();
            Destroy(gameObject);
        }
    }
}
