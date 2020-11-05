using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("Количество очков за уничтожение блока")] 
    public int points;

    GameManager gameManager;
    LevelManager levelManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();

        levelManager.BlockCreated();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.AddScore(points);
        levelManager.BlockDestroyed();
        Destroy(gameObject);
    }
}
