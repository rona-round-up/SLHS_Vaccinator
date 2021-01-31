using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Color beatHigh;
    EnemySpawner controller;
    public Text high;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
        high.GetComponent<Text>().text = "High: " + PlayerPrefs.GetInt("Highscore", 0);
    }

    void Update()
    {
        GetComponent<Text>().text = controller.score + "";
        if (controller.score > PlayerPrefs.GetInt("Highscore", 0))
        {
            GetComponent<Text>().color = beatHigh;
            PlayerPrefs.SetInt("Highscore", controller.score);
            high.GetComponent<Text>().text = "High: " + PlayerPrefs.GetInt("Highscore", 0);
        }
    }
}
