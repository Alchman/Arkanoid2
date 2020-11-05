using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int blocksCount;

    //private void Start()
    //{
    //    Block[] allBlocks = FindObjectsOfType<Block>();
    //    blocksCount = allBlocks.Length;
    //}

    public void BlockCreated()
    {
        blocksCount++;
    }

    public void BlockDestroyed()
    {
        blocksCount--;
        if (blocksCount <= 0)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
    }
}
