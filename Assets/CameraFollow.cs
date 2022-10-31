using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!GameManager.instance.GetIsReplayMode()) {
            transform.position = target.position + offset;
        }
    }
}
