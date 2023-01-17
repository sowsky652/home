using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public GameObject prefab;
    private TextMeshProUGUI damageTextprefab;

    public void Awake()
    {
        damageTextprefab = (TextMeshProUGUI)Resources.Load("Damage");
    }
    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null)
            return;

        var distacne = Vector3.Distance(attacker.transform.position, defender.transform.position);
        if (distacne > range)
            return;

        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();
        if (Vector3.Dot(attacker.transform.forward, dir) < 0.5f)
            return;

        // ����
        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CreateAttack(aStats, dStats);
        var attackables = defender.GetComponentsInChildren<IAttakable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }


        //var enemypos = Camera.main.WorldToScreenPoint(defender.transform.position);

        //var temp = Instantiate(damageTextprefab, enemypos, Quaternion.identity, GameObject.Find("Canvas").transform);

        //Camera.main.ScreenToWorldPoint(temp.transform.position);

        //damageTextprefab.text = aStats.damage.ToString();

    }
}
