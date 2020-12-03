using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    public float padSpeed = 1f;

    public bool keyboard;
    public bool autoplay;
    public float maxX;

    float yPosition;
    Ball ball;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ball = FindObjectOfType<Ball>();

        yPosition = transform.position.y;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.pauseActive)
        {
            //пауза активна - ничего не нужно делать
            return;
        }

        Movement();
    }

    private void Movement()
    {
        Vector3 padNewPosition;
        if (autoplay)
        {
            Vector3 ballPos = ball.transform.position;
            padNewPosition = new Vector3(ballPos.x, yPosition, 0);
        }
        if (keyboard)
        {
            //keyaboard movement
            padNewPosition = transform.position;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                padNewPosition.x += padSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                padNewPosition.x -= padSpeed * Time.deltaTime;
            }
        }
        else
        {
            //mouse movement
            Vector3 mousePixelPosition = Input.mousePosition; //позиция мыши в координатах экрана
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelPosition); //позиция мыши в координатах игрового мира

            padNewPosition = new Vector3(mouseWorldPosition.x, yPosition, 0);
        }

        padNewPosition.x = Mathf.Clamp(padNewPosition.x, -maxX, maxX);
        transform.position = padNewPosition;
    }
}
