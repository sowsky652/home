using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public float distance = 1f;

    public Doorway other;

    public Vector3 OtherPosition
    {
        get
        {
            if (other == null)
                return transform.position;

            var pos = other.transform.position;
            pos += transform.forward * distance;
            return pos;
        }
    }

    private void OnDrawGizmos()
    {
        if (other != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(OtherPosition, 0.1f);
        }
    }
}
