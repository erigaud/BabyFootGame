using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _sensitivity_rotation;
    private float _sensitivity_translation;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private float _rotation;
    private float _translation;
    private Rigidbody rbody;
    private bool _isRotating;

    void Start()
    {
        _sensitivity_rotation = 5f;
        _sensitivity_translation = 0.05f;
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation = -(_mouseOffset.x) * _sensitivity_rotation;
            _translation = -(_mouseOffset.y) * _sensitivity_translation;
            // rotate

            rbody.velocity = new Vector3(_translation / Time.fixedDeltaTime, 0, 0);
            rbody.angularVelocity = new Vector3(_rotation / Time.fixedDeltaTime, 0, 0);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
        else {
            rbody.velocity = new Vector3(0, 0, 0);
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        Debug.Log("mouse down");
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }
}
