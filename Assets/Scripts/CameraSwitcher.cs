using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform target;
    public float secondsToMove = 1;

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(Move(target));
    }

    private IEnumerator Move(Transform newPos) {
        //_isMoving = true;
        Vector3 startPos = Camera.main.transform.position;
        Quaternion startRot = Camera.main.transform.rotation;

        Vector3 endPos = newPos.position;
        Quaternion endRot = newPos.rotation;

        float time = 0;
        float increase = 0.01f / secondsToMove;
        while (time < 1) {
            yield return new WaitForSeconds(0.01f);
            time += increase;
            Camera.main.transform.position = Vector3.Lerp(startPos, endPos, time);
            Camera.main.transform.rotation = Quaternion.Lerp(startRot, endRot, time);
        }
        //_isMoving = false;
    }
}
