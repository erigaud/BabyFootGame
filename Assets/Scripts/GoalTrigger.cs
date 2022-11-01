using TMPro;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball" && !GameManager.instance.GetIsReplayMode()) {
            GameManager.instance.AddSideScore(scoreText);
        }
    }
}