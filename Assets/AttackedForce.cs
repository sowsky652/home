using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedForce : MonoBehaviour, IAttakable
{
    public float up = 1f;
    public float power = 10f;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var dir = transform.position - attacker.transform.position;
        dir.y += up;
        dir.Normalize();
        rigid.AddForce(dir * power, ForceMode.Impulse);
    }
}
