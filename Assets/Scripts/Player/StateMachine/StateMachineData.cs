using System;
using UnityEngine;

public class StateMachineData 
{
    public Vector2 Velocity;
    public bool IsSprinting;
    public float Acceleration;

    private float _speed;
    private float _drag;
    private float _cameraRotationSpeed;
    private Vector2 _input;

    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _speed = value;
        }
    }

    public Vector2 Input
    {
        get => _input;
        set
        {
            if (value.x > 1 ||  value.y > 1 || value.x < -1 || value.y < -1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _input = value;
        }
    }

    public float CameraRotationSpeed 
    { 
        get => _cameraRotationSpeed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _cameraRotationSpeed = value;
        }
    }

    public float Drag 
    { 
        get => _drag;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _drag = value;
        }
    }
}
