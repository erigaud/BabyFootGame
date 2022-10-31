using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isReplayMode;
    public static bool isEndGame;
    public static GameManager instance;

    public GameObject ballPrefab;
    public GameObject ballInstance;

    public Camera cam1;
    public Camera cam2;

    private bool mustSpawnBall = false;
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

        //if (mustSpawnBall)
        //{
        //    SpawnBall();
        //    mustSpawnBall = false;
        //}
        if (isEndGame)
        {
            return;
        }
    }
    public bool GetIsReplayMode()
    {
        return isReplayMode;
    }
    public void Engagement() {
        //Debug.Log("Engagement");
        SpawnBall();
        int side = Random.Range(0, 2) * 2 - 1;
        Vector3 force = new Vector3(Random.Range(0, 2) * 2 - 1, 0,(0.05f * side));
        ballInstance.GetComponent<Rigidbody>().AddForce(force * 30);
    }

    public void SpawnBall() {
        //Debug.Log("Spawning ball");
        ballInstance = (GameObject)Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        cam2 = GameObject.Find("BallCam").GetComponent<Camera>();
        SetTargetToReplay(ballInstance.transform);
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
        EnterReplayMode();
    }

    public void EnterReplayMode()
    {
        isReplayMode = true;
        cam1.enabled = false;
        cam2.enabled = true;
    }

    public void ExitReplayMode()
    {
        isReplayMode = false;
        cam1.enabled = true;
        cam2.enabled = false;
        mustSpawnBall = true;
        DestroyBall();
        SpawnBall();
    }

    public void SetTargetToReplay(Transform _transform) {
        GameObject.Find("BallCam").GetComponent<CameraFollow>().target = _transform;
    }
}
