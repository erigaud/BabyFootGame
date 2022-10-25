using TMPro;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    private void OnTriggerExit(Collider other)
    {
        GameManager.instance.DestroyBall();
        GameManager.instance.AddSideScore(scoreText);
    }
}