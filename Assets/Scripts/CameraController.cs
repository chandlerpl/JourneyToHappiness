using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController Instance
    {
        get => _instance;
    }
    
    public Transform[] cameraPositions;
    public float secondsToMove = 1;
    private int _currentPosition;

    private bool _isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            enabled = false;
            return;
        }
        _instance = this;
        
        transform.position = cameraPositions[_currentPosition].position;
        transform.rotation = cameraPositions[_currentPosition].rotation;
    }

    public void MoveRight()
    {
        if (_isMoving) return;
        if (++_currentPosition >= cameraPositions.Length)
        {
            _currentPosition = 0;
        }

        StartCoroutine(Move(cameraPositions[_currentPosition]));
    }

    private IEnumerator Move(Transform newPos)
    {
        _isMoving = true;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        
        Vector3 endPos = newPos.position;
        Quaternion endRot = newPos.rotation;

        float time = 0;
        float increase = 0.01f / secondsToMove;
        while (time < 1)
        {
            yield return new WaitForSeconds(0.01f);
            time += increase;
            transform.position = Vector3.Lerp(startPos, endPos, time);
            transform.rotation = Quaternion.Lerp(startRot, endRot, time);
        }
        _isMoving = false;
    }

    public void MoveLeft()
    {
        if (_isMoving) return;
        if (--_currentPosition < 0)
        {
            _currentPosition = cameraPositions.Length - 1;
        }

        StartCoroutine(Move(cameraPositions[_currentPosition]));
    }
}
