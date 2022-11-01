using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamelleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public string side;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if ((side == "blue" && rb.velocity.z < 0) || (side == "red" && rb.velocity.z > 0))
                {
                    GameManager.instance.RemoveSideScore(scoreText);
                }
            }
    }
}
