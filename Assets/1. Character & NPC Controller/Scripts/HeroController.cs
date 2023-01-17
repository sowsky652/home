using System.Collections;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HeroController : MonoBehaviour
{
    public static readonly int HashSpeed = Animator.StringToHash("Speed");
    public static readonly int HashAttack= Animator.StringToHash("Attack");

    public AttackDefinition demoAttack;

    private GameObject attackTarget;
    private float lastAttakTime = 0f;
    private Coroutine coAttackAndMove;

    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent
    private CharacterStats stats;

    private CharacterInventory inventory;
    public TextMeshProUGUI damageTextprefab;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        inventory = GetComponent<CharacterInventory>();
    }

    private void Update()
    {
        animator.SetFloat(HashSpeed, agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 pos)
    {
        StopCoMoveAndAttack();

        agent.isStopped = false;
        agent.SetDestination(pos);
    }

    public void AttackTarget(GameObject target)
    {
        StopCoMoveAndAttack();

        attackTarget = target;
        coAttackAndMove = StartCoroutine(CoMoveAndAttack());
    }

    void StopCoMoveAndAttack()
    {
        if (coAttackAndMove != null)
        {
            attackTarget = null;
            StopCoroutine(coAttackAndMove);
            coAttackAndMove = null;
        }
    }

    IEnumerator CoMoveAndAttack()
    {
        // 1. Ÿ�� ����
        var targetPos = attackTarget.transform.position;
        var distance = Vector3.Distance(transform.position, targetPos);
        agent.isStopped = false;

        var range = inventory.CurrentWeapon != null ? inventory.CurrentWeapon.range : 1f;
        while (distance > range)
        {
            agent.SetDestination(targetPos);
            yield return null;
            distance = Vector3.Distance(transform.position, targetPos);
        }
        agent.isStopped = true;

        // 2. Ÿ�� �ٶ󺸰�
        var lookPos = targetPos;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        // 3. ���� �ִϸ��̼� ���� (CoolDown)
        if (inventory.CurrentWeapon != null &&
            Time.time - lastAttakTime > inventory.CurrentWeapon.coolDown)
        {
            lastAttakTime = Time.time;
            animator.SetTrigger(HashAttack);
        }
    }

    public void Hit()
    {
        if (inventory.CurrentWeapon == null)
            return;

        inventory.CurrentWeapon.ExecuteAttack(gameObject, attackTarget);

        var enemypos = Camera.main.WorldToScreenPoint(attackTarget.transform.position);

        var temp = Instantiate(damageTextprefab, enemypos, Quaternion.identity, GameObject.Find("Canvas").transform);

        Camera.main.ScreenToWorldPoint(temp.transform.position);

        damageTextprefab.text = inventory.CurrentWeapon.minDamage.ToString();

        attackTarget = null;
    }
}
