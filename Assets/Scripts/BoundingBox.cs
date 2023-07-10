using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public Vector3 centre;
    public Vector3 size = new Vector3(1, 1, 1);

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(centre, size);
    }
}

