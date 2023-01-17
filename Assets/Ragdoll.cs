using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ragdoll : MonoBehaviour
{
    public float duration = 5f;
    public Rigidbody spineRb;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    public void ApplyForce(Vector3 force)
    {
        spineRb.AddForce(force, ForceMode.Impulse);
    }
}
