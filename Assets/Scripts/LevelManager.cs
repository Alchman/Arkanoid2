using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int blocksCount;


    public void BlockCreated()
    {
        blocksCount++;
    }

    public void BlockDestroyed()
    {
        blocksCount--;
        if (blocksCount <= 0)
        {
            //УРОВЕНЬ ПРОЙДЕН
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.SaveBestScore();
        }
    }
}
