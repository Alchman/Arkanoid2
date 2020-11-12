using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("Количество очков за уничтожение блока")] 
    public int points;
    public bool invisible;

    [Header("Prefabs")]
    public GameObject pickupPrefab;
    public GameObject particleEffectPrefab;

    GameManager gameManager;
    LevelManager levelManager;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        levelManager.BlockCreated();

        if (invisible)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invisible)
        {
            spriteRenderer.enabled = true;
            invisible = false;
            return;
        }

        DestroyBlock();
    }

    private void DestroyBlock()
    {
        gameManager.AddScore(points);
        levelManager.BlockDestroyed();
        Destroy(gameObject);

        //создать объект на основе префаба
        Instantiate(pickupPrefab, transform.position, Quaternion.identity); 
        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity); 
    }
}
