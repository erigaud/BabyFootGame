using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isEndGame;
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Problem multiple game manager");
        }
        instance = this;
    }

    private void Start()
    {
        isEndGame = false;
    }

    private void Update()
    {
        if (isEndGame)
        {
            return;
        }
    }

    public void SpawnBall() {
        Debug.Log("Respawning ball");
    }

    public void RemoveSideScore(TextMeshProUGUI scoreText) {
        int newScore = int.Parse(scoreText.text) - 1;
        scoreText.text = newScore.ToString();
    }

    public void AddSideScore(TextMeshProUGUI scoreText)
    {
        Debug.Log("Goal");
        int newScore = int.Parse(scoreText.text) + 1;
        scoreText.text = newScore.ToString();
        SpawnBall();
    }
}
