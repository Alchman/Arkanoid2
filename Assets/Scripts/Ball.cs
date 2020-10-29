using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    public Pad pad;

    bool isStarted;
    float yPosition;

    void Start()
    {
        yPosition = transform.position.y;
    }

    private void Update()
    {
        if (isStarted)
        {
            //мяч запущен
            //ничего не делаем
        }
        else
        {
            //мяч ещё не запущен

            //двигаться вместе с платформой
            Vector3 padPosition = pad.transform.position; //позиция платформы

            Vector3 ballNewPosition = new Vector3(padPosition.x, yPosition, 0); //новая позиция мяча
            transform.position = ballNewPosition;

            //проверить левую кнопку мыши
            if (Input.GetMouseButtonDown(0)) //левая клавиша мыши
            {
                StartBall();
            }
        }

        //print(rb.velocity);
    }

    private void StartBall()
    {
        Vector2 force = new Vector2(1, 1) * speed;
        rb.AddForce(force);
        isStarted = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("Collision!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("Collision exit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Trigger!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("Trigger exit");
    }
}
