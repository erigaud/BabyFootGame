using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < 0.40f) {
            //Debug.Log("Add lil force");
        }
    }
}