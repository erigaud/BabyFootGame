using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamelleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    private void OnTriggerExit(Collider other)
    {
        int newScore;
        Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb.velocity.z > 0)
        {
        newScore = int.Parse(scoreText.text) - 1;
        scoreText.text = newScore.ToString();
        } 

    }
}
