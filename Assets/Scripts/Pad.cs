using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
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

        Vector3 padNewPosition;
        if (autoplay)
        {
            Vector3 ballPos = ball.transform.position;
            padNewPosition = new Vector3(ballPos.x, yPosition, 0);
        }
        else
        {
            Vector3 mousePixelPosition = Input.mousePosition; //позиция мыши в координатах экрана
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelPosition); //позиция мыши в координатах игрового мира

            padNewPosition = new Vector3(mouseWorldPosition.x, yPosition, 0);
        }

        padNewPosition.x = Mathf.Clamp(padNewPosition.x, -maxX, maxX);
        transform.position = padNewPosition;
    }
}
