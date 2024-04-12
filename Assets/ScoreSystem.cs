using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    void Start()
    {

    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Run>().isGameOver)
        {
            if (PlayerPrefs.GetInt("HightScore") < score)
            {
                PlayerPrefs.SetInt("HightScore", score);
                Debug.Log("New Hight Socre: " + score);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            score = score + 1;
            scoreText.text = "Score: " + score;
        }
    }

}
