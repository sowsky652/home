using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDefinition : ScriptableObject
{
    public float coolDown;
    public float range;
    public float minDamage;
    public float maxDamage;
    public float criticalChance; // 0.0 ~ 1.0
    public float criticalMultiplayer; // 1.0 100%

    public Attack CreateAttack(CharacterStats attacker, CharacterStats defender)
    {
        float damage = attacker.damage;
        damage += Random.Range(minDamage, maxDamage);
        var isCritical = Random.value < criticalChance;
        if (isCritical)
        {
            damage *= criticalMultiplayer;
        }

        if (defender != null)
        {
            damage -= defender.armor;
        }

        return new Attack((int)damage, isCritical);
    }
}
