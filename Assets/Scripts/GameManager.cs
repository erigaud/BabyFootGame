using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isReplayMode;
    private TextMeshProUGUI replayText;

    public static bool isEndGame;
    public static GameManager instance;
    public GameObject ballPrefab;
    public GameObject ballInstance;
    public Camera cam1;
    public Camera cam2;
    public Light principalLight;
    public int maxNumberOfBalls;

    public bool isMultiBallEvenement;
    public bool isBallLightEvenement;

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
        isMultiBallEvenement = false;
        isBallLightEvenement = false;
        replayText = GameObject.Find("ReplayText").GetComponent<TextMeshProUGUI>();
        Engagement();
        //StartCoroutine(BallLightEvenement(20.0f));
        //StartCoroutine(MultiBallEvenement());
    }

    private void Update()
    {
        if (isEndGame)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void GetRandomEvent() { 
    
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

    public void DestroyBall(GameObject gameObject) {
        //Debug.Log("Destroying ball");
        Destroy(gameObject);
    }

    public void RemoveSideScore(TextMeshProUGUI scoreText) {
        int newScore = int.Parse(scoreText.text) - 1;
        scoreText.text = newScore.ToString();
    }

    public void AddSideScore(string side, Collider other)
    {
        //Debug.Log("Goal");
        TextMeshProUGUI scoreText = GameObject.Find(side + "Score").GetComponent<TextMeshProUGUI>();
        int newScore = int.Parse(scoreText.text) + 1;
        scoreText.text = newScore.ToString();
        if (NumberOfBalls() == 1) {
            EnterReplayMode();
        }
        else {
            DestroyBall(other.gameObject);
        }
        if (newScore >= 10) {
            isEndGame = true;
        }
    }

    public void EnterReplayMode()
    {
        isReplayMode = true;
        cam1.enabled = false;
        cam2.enabled = true;
        replayText.enabled = true;
    }

    public void ExitReplayMode()
    {
        isReplayMode = false;
        cam1.enabled = true;
        cam2.enabled = false;
        replayText.enabled = false;
        DestroyBall(ballInstance.gameObject);
        SpawnBall();
    }

    public int NumberOfBalls() {
        GameObject[] listOfBall = GameObject.FindGameObjectsWithTag("Ball");
        return listOfBall.Length;
    }

    public void SetTargetToReplay(Transform _transform) {
        GameObject.Find("BallCam").GetComponent<CameraFollow>().target = _transform;
    }

    public IEnumerator BallLightEvenement(float delay) {
        principalLight.enabled = false;
        GameObject.Find("BallLight").GetComponent<Light>().enabled = true;
        cam1.backgroundColor = Color.black;
        yield return new WaitForSeconds(delay);
        principalLight.enabled = true;
        GameObject.Find("BallLight").GetComponent<Light>().enabled = false;
        cam1.backgroundColor = Color.blue;
    }

    public IEnumerator MultiBallEvenement() {
        isMultiBallEvenement = true;
        for (int i =0; i<maxNumberOfBalls; i++) {
            GameObject evenementBallInstance = (GameObject)Instantiate(ballPrefab, ballInstance.transform.position, Quaternion.identity);
            int side = Random.Range(0, 2) * 2 - 1;
            Vector3 force = new Vector3(Random.Range(0, 2) * 2 - 1, 0, (0.05f * side));
            evenementBallInstance.GetComponent<Rigidbody>().AddForce(force * 30);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
