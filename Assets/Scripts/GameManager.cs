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
    private GameObject ballInstance;

    public Camera cam1;
    public Light principalLight;
    public int maxNumberOfBalls;

    private float timer;
    private float intervalGenerateEvents;
    public Dictionary<string, int> dictEventActive;
    private List<string> listOfEvents;

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
        timer = 0;
        intervalGenerateEvents = 6.0f;
        isEndGame = false;
        listOfEvents = new List<string>();
        listOfEvents.Add("MultiBallEvenement");
        listOfEvents.Add("BallLightEvenement");
        dictEventActive = new Dictionary<string, int>();
        dictEventActive["MultiBallEvenement"] = 0;
        dictEventActive["BallLightEvenement"] = 0;
        replayText = GameObject.Find("ReplayText").GetComponent<TextMeshProUGUI>();
        Engagement();
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
        if (timer >= intervalGenerateEvents && !GetIsReplayMode()) {
            timer = 0;
            string nameEvent = GetRandomEvent();
            Debug.Log(nameEvent);
            if (nameEvent == "MultiBallEvenement") {
                StartCoroutine(MultiBallEvenement());
            }
            else if (nameEvent == "BallLightEvenement") {
                StartCoroutine(BallLightEvenement(20.0f));
            }
        }
        else
            timer += Time.deltaTime;
    }

    public string GetRandomEvent() {
        float rd = Random.Range(0, 100);
        if (rd <= 80){
            int rdEvent = Random.Range(0, listOfEvents.Count);
            string eventName = listOfEvents[rdEvent];
            if (dictEventActive[eventName] == 0) {
                return eventName;
            }
        }
        return "None";
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
        ballInstance.transform.Find("BallBody").GetComponent<Rigidbody>().AddForce(force * 30);
    }

    public void SpawnBall() {
        Debug.Log("Spawning ball");

        ballInstance = (GameObject)Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void DestroyBall(GameObject gameObject) {
        Debug.Log("Destroying ball");
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
        Debug.Log(NumberOfBalls());
        GameObject ball = GameObject.FindGameObjectWithTag("Ball").transform.parent.gameObject;
        if (NumberOfBalls() == 1) {
            dictEventActive["MultiBallEvenement"] = 0;
            EnterReplayMode(ball);
        }
        else {
            DestroyBall(other.transform.parent.gameObject);
        }
        if (newScore >= 10) {
            isEndGame = true;
        }
    }

    public void EnterReplayMode(GameObject ball)
    {
        isReplayMode = true;
        cam1.enabled = false;
        ball.GetComponentInChildren<Camera>().enabled = true;
        replayText.enabled = true;
    }

    public void ExitReplayMode(GameObject ball)
    {
        isReplayMode = false;
        cam1.enabled = true;
        ball.GetComponentInChildren<Camera>().enabled = false;
        replayText.enabled = false;
        DestroyBall(ball.gameObject);
        SpawnBall();
    }

    public int NumberOfBalls() {
        GameObject[] listOfBall = GameObject.FindGameObjectsWithTag("Ball");
        return listOfBall.Length;
    }

    public IEnumerator BallLightEvenement(float delay) {
        dictEventActive["BallLightEvenement"] = 1;
        principalLight.enabled = false;
        cam1.backgroundColor = Color.black;
        EnableLightOnBalls();
        yield return new WaitForSeconds(delay);
        DisableLightOnBalls();
        cam1.backgroundColor = Color.blue;
        dictEventActive["BallLightEvenement"] = 0;
        principalLight.enabled = true;
    }

    public IEnumerator MultiBallEvenement() {
        dictEventActive["MultiBallEvenement"] = 1;
        for (int i =0; i<maxNumberOfBalls; i++) {
            GameObject evenementBallInstance = (GameObject)Instantiate(ballPrefab, ballInstance.transform.position, Quaternion.identity);
            int side = Random.Range(0, 2) * 2 - 1;
            Vector3 force = new Vector3(Random.Range(0, 2) * 2 - 1, 0, (0.05f * side));
            evenementBallInstance.transform.Find("BallBody").GetComponent<Rigidbody>().AddForce(force * 30);
            yield return new WaitForSeconds(2.0f);
        }
        if (dictEventActive["BallLightEvenement"] == 1) {
            EnableLightOnBalls();
        }
    }

    public void EnableLightOnBalls() {
        GameObject[] listOfBall = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < listOfBall.Length; i++)
        {
            listOfBall[i].GetComponentInChildren<Light>().enabled = true;
            listOfBall[i].GetComponentInChildren<Light>().enabled = true;
        }
    }

    public void DisableLightOnBalls() {
        GameObject[] listOfBall = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < listOfBall.Length; i++)
        {
            listOfBall[i].GetComponentInChildren<Light>().enabled = false;
            listOfBall[i].GetComponentInChildren<Light>().enabled = false;
        }
    }
}
