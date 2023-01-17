using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDestoryObject : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        Destroy(gameObject);
    }
}
