using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print("Collision exit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Trigger exit");
    }
}
