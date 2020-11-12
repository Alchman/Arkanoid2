using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    
    Pad pad;

    bool isStarted;
    float yPosition;

    void Start()
    {
        pad = FindObjectOfType<Pad>();

        yPosition = transform.position.y;

        if (pad.autoplay)
        {
            StartBall();
        }
    }

    public void Restart()
    {
        isStarted = false;
        rb.velocity = Vector2.zero; //new Vector2(0, 0);
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
        print(rb.velocity.magnitude);
    }

    private void StartBall()
    {
        float randomX = Random.Range(0, 0);
        Vector2 direction = new Vector2(randomX, 1);
        Vector2 force = direction.normalized * speed;

        //rb.AddForce(force);
        rb.velocity = force;

        isStarted = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, rb.velocity);
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
