using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    private void OnTriggerExit(Collider other)
    {
        int newScore = int.Parse(scoreText.text) + 1;
        scoreText.text = newScore.ToString();
    }
}