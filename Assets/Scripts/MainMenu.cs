using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int bestScore = PlayerPrefs.GetInt(GameManager.keyBestScore, 1000);

        bestScoreText.text = "BEST RECORD: " + bestScore;
    }
}
