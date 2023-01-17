using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttakable 
{
    void OnAttack(GameObject attacker, Attack attack);
}
