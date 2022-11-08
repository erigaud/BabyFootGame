using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject cam;
    private GameObject body;
    public Vector3 offset;
    private void Start()
    {
        cam = this.transform.Find("BallCam").gameObject;
        body = this.transform.Find("BallBody").gameObject;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        cam.transform.position = body.transform.position + offset;
    }
}
