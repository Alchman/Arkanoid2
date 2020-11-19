using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("Количество очков за уничтожение блока")] 
    public int points;
    public bool invisible;

    [Header("Explosive")]
    public bool explosive;
    public float explosionRadius;

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

        if (explosive)
        {
            //блок взрывной - логика взрыва
            Explode();
        }
    }

    private void Explode()
    {
        int layerMask = LayerMask.GetMask("Block");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);

        //for(int i = 0; i < colliders.Length; i++)
        //{
        //    print(colliders[i].name);
        //}

        foreach(Collider2D col in colliders)
        {
            print(col.name);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
