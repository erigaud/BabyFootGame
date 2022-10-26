using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isEndGame;
    public static GameManager instance;

    public GameObject ballPrefab;
    public GameObject ballInstance;

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
        Engagement();
    }

    private void Update()
    {
        if (isEndGame)
        {
            return;
        }
    }

    public void Engagement() {
        //Debug.Log("Engagement");
        SpawnBall();
        int side = Random.Range(0, 2) * 2 - 1;
        Vector3 force = new Vector3(Random.Range(0, 2) * 2 - 1, 0,(0.05f * side));
        ballInstance.GetComponent<Rigidbody>().AddForce(force * 1500);
    }

    public void SpawnBall() {
        //Debug.Log("Spawning ball");
        ballInstance = (GameObject)Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void DestroyBall() {
        //Debug.Log("Destroying ball");
        Destroy(ballInstance);
    }

    public void RemoveSideScore(TextMeshProUGUI scoreText) {
        int newScore = int.Parse(scoreText.text) - 1;
        scoreText.text = newScore.ToString();
    }

    public void AddSideScore(TextMeshProUGUI scoreText)
    {
        //Debug.Log("Goal");
        int newScore = int.Parse(scoreText.text) + 1;
        scoreText.text = newScore.ToString();
        SpawnBall();
    }
}
