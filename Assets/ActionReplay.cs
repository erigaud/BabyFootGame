using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    private GameManager gameManager;
    private bool isInReplayMode;
    private Rigidbody rigidBody;
    private int currentReplayIndex;
    public Camera cam1;
    public Camera cam2;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameManager.GetIsReplayMode());
        if(Input.GetKeyDown(KeyCode.R))
        {
            isInReplayMode = !isInReplayMode;
            if (isInReplayMode)
            {
                cam1.enabled =false;
                cam2.enabled = true;
                SetTransform(0);
                rigidBody.isKinematic = true;
            } else
            {
                SetTransform(actionReplayRecords.Count - 1);
                rigidBody.isKinematic =false;
                cam1.enabled = true;
                cam2.enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {

        if (!isInReplayMode)
        {
            actionReplayRecords.Add(new ActionReplayRecord
            {
                position = transform.position,
                rotation = transform.rotation
            });
        } else
        {
            int nextIndex = currentReplayIndex + 1;

            if (nextIndex < actionReplayRecords.Count)
            { 
                SetTransform(nextIndex);
            }
        }

        
    }

    private void SetTransform(int index)
    {
        currentReplayIndex = index;
        ActionReplayRecord actionReplayRecord = actionReplayRecords[index];
        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }
}
