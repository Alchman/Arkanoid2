using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string keyBestScore = "bestRecord";

    public Text scoreText;
    public GameObject panelPause;

    [Header("Sounds")]
    public AudioClip sndPauseActivate;
    public AudioClip sndPauseDeactivate;

    [HideInInspector]
    public bool pauseActive;

    int score;
    int lifes;

    AudioManager audioManager;

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

        audioManager = FindObjectOfType<AudioManager>();
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

    public void SaveBestScore()
    {
        int oldBestScore = PlayerPrefs.GetInt(keyBestScore);
        if(score > oldBestScore)
        {
            PlayerPrefs.SetInt(keyBestScore, score);
        }
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

                audioManager.PlaySound(sndPauseDeactivate);
            }
            else
            {
                //включить паузу
                Time.timeScale = 0f;
                pauseActive = true;

                audioManager.PlaySound(sndPauseActivate);
            }
            panelPause.SetActive(pauseActive);
        }
    }
}
