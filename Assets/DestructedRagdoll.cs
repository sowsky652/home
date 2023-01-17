using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructedRagdoll : MonoBehaviour, IDestructible
{
    public Ragdoll prefab;

    public float power = 10f;
    public float lift = 1f;

    public void OnDestruction(GameObject attacker)
    {
        var ragdoll = Instantiate(prefab, transform.position, transform.rotation);

        var dir = transform.position - attacker.transform.position;
        dir.y += lift;
        dir.Normalize();

        ragdoll.ApplyForce(dir * power);
    }
}
