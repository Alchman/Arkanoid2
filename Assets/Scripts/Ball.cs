using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    
    Pad pad;

    bool isStarted;
    bool isMagnetActive;

    float yPosition;
    float xDelta;

    public void ActivateMagnet()
    {
        isMagnetActive = true;
    }

    public void MultiplySpeed(float speedKoef)
    {
        speed *= speedKoef;
        rb.velocity = rb.velocity.normalized * speed;
    }

    void Start()
    {
        pad = FindObjectOfType<Pad>();

        yPosition = transform.position.y;
        xDelta = transform.position.x - pad.transform.position.x;

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

    public void Duplicate()
    {
        Ball originalBall = this;

        Ball newBall = Instantiate(originalBall);

        newBall.speed = speed;
        newBall.StartBall();

        if (isMagnetActive)
        {
            newBall.ActivateMagnet();
        }
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

            Vector3 ballNewPosition = new Vector3(padPosition.x + xDelta, yPosition, 0); //новая позиция мяча
            transform.position = ballNewPosition;

            //проверить левую кнопку мыши
            if (Input.GetMouseButtonDown(0)) //левая клавиша мыши
            {
                StartBall();
            }
        }

        //print(rb.velocity);
        //print(rb.velocity.magnitude);
    }

    private void StartBall()
    {
        float randomX = Random.Range(0, 0);
        Vector2 direction = new Vector2(randomX, 1);
        Vector2 force = direction.normalized * speed;

        rb.velocity = force;
        //rb.AddForce(force);

        isStarted = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMagnetActive && collision.gameObject.CompareTag("Pad"))
        {
            yPosition = transform.position.y;
            xDelta = transform.position.x - pad.transform.position.x;
            Restart();
        }
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
