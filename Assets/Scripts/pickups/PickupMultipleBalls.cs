using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMultipleBalls : MonoBehaviour
{
    private void ApplyEffect()
    {
        Ball ball = FindObjectOfType<Ball>();
        ball.Duplicate();
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
