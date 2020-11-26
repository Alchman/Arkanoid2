using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float explosionRadius;
    public float speed;

    public GameObject explosionEffect;
    public AudioClip explosionModeSound;
    
    Rigidbody2D rb;
    AudioSource audioSource;

    Pad pad;

    bool isStarted;
    bool isMagnetActive;
    bool isExplosive; //активен ли режим взрыва

    float yPosition;
    float xDelta;

    private void Awake()
    {
        //Поиск компонентов на этом же GameObject лучше делать в Awake
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateMagnet()
    {
        isMagnetActive = true;
    }

    public void ActivateExplosion()
    {
        isExplosive = true;
        explosionEffect.SetActive(true);
        audioSource.clip = explosionModeSound;
        //TODO поменять цвет trail и спрайт
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

        if (isExplosive)
        {
            newBall.ActivateExplosion();
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
        if (Application.isPlaying)
        {
            Gizmos.DrawRay(transform.position, rb.velocity);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();

        if (isMagnetActive && collision.gameObject.CompareTag("Pad"))
        {
            yPosition = transform.position.y;
            xDelta = transform.position.x - pad.transform.position.x;
            Restart();
        }

        if (isExplosive && collision.gameObject.CompareTag("Block"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        int layerMask = LayerMask.GetMask("Block");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position, 
            explosionRadius, 
            layerMask);

        foreach (Collider2D col in colliders)
        {
            Block block = col.GetComponent<Block>(); //Пытаемся найти у коллайдера скрипт Block
            if (block == null)
            {
                //объект без скрипта Block - просто уничтожаем
                Destroy(col.gameObject);
            }
            else
            {
                //Объект со скриптом Block
                block.DestroyBlock();
            }
        }
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
