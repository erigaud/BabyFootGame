using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    private bool isInReplayMode;
    private Rigidbody rigidBody;
    private int currentReplayIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isInReplayMode = GameManager.instance.GetIsReplayMode();
        if (isInReplayMode && currentReplayIndex == 0)
        {
            Debug.Log("lock");
            SetTransform(0);
            rigidBody.isKinematic = true;
        }
        else if (currentReplayIndex == actionReplayRecords.Count - 1 && currentReplayIndex != 0)
        {
            SetTransform(actionReplayRecords.Count - 1);
            rigidBody.isKinematic = false;
            Debug.Log("unlock");
            currentReplayIndex = 0;
            if (isInReplayMode && this.tag == "Ball")
            {
                GameManager.instance.ExitReplayMode(this.transform.parent.gameObject);
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
