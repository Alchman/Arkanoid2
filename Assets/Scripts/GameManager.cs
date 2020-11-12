using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject panelPause;

    [HideInInspector]
    public bool pauseActive;

    int score;
    int lifes;

    private void Awake()
    {
        GameManager[] gameManagers = FindObjectsOfType<GameManager>();
        for (int i = 0; i < gameManagers.Length; i++)
        {
            if (gameManagers[i].gameObject != gameObject)
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
                break;
            }

        }
    }

    private void Start()
    {
        scoreText.text = "000";

        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString();
    }

    public void LoseLife()
    {
        lifes--;

        if (lifes <= 0)
        {
            //TODO restart
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pauseActive)
            {
                //пауза уже активна - вернуть время в 1
                Time.timeScale = 1f;
                pauseActive = false;
            }
            else
            {
                //включить паузу
                Time.timeScale = 0f;
                pauseActive = true;
            }
            panelPause.SetActive(pauseActive);
        }
    }
}
