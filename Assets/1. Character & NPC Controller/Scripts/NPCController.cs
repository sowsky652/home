using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public enum States
    {
        None = -1,
        Idle,
        Patrol,
        Chase,
        Attack,
        GameOver,
    }

    public Weapon attackDefinition;

    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area
    private int index = 0;
    //private Transform waypoint;

    public float speed;
    public float chaseSpeed; // current agent speed and NavMeshAgent component speed

    private Transform player; // reference to the player object transform

    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private float distanceToPlayer;
    private float distanceToWayPoint;
    public float idleTime = 1f;
    public float chaseInterval = 0.25f;
    private float timer = 0f;

    private States state = States.None;

    public States State
    {
        get { return state; }
        private set
        {
            var prevState = state;
            state = value;

            if (prevState == state)
                return;

            switch (state)
            {
                case States.Idle:
                    timer = 0f;
                    agent.speed = speed;
                    agent.isStopped = true;
                    break;
                case States.Patrol:
                    agent.speed = speed;
                    agent.isStopped = false;
                    index = (int)Mathf.Repeat(index + 1, waypoints.Length);
                    agent.SetDestination(waypoints[index].position);
                    break;
                case States.Chase:
                    timer = 0f;
                    agent.speed = chaseSpeed;
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                    break;
                case States.Attack:
                    timer = attackDefinition.coolDown;
                    agent.isStopped = true;
                    break;
                case States.GameOver:
                    agent.isStopped = true;
                    break;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        var ev = player.GetComponent<DestructedEvent>();
        ev.OnDie += OnPlayerDie;
    }

    public void OnPlayerDie()
    {
        State = States.GameOver;
    }

    private void Start()
    {
        State = States.Idle;
    }

    private void Update()
    {
        if (State != States.GameOver)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            var wayPoint = waypoints[index].position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(wayPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                wayPoint = hit.position;
            }
            distanceToWayPoint = Vector3.Distance(transform.position, wayPoint);
        }

        switch (State)
        {
            case States.Idle:
                UpdateIdle();
                break;
            case States.Patrol:
                UpdatePatrol();
                break;
            case States.Chase:
                UpdateChase();
                break;
            case States.Attack:
                UpdateAttack();
                break;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void UpdateAttack()
    {
        timer += Time.deltaTime;

        if (distanceToPlayer > attackDefinition.range)
        {
            State = States.Chase;
            return;
        }

        if (timer > attackDefinition.coolDown)
        {
            timer = 0f;

            var lookPos = player.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
            animator.SetTrigger("Attack");
        }
    }

    public void Hit()
    {
        attackDefinition.ExecuteAttack(gameObject, player.gameObject);
    }

    private void UpdateChase()
    {
        timer += Time.deltaTime;

        if (distanceToPlayer < attackDefinition.range)
        {
            State = States.Attack;
            return;
        }

        if (distanceToPlayer > aggroRange)
        {
            State = States.Idle;
            return;
        }

        if (timer > chaseInterval)
        {
            agent.SetDestination(player.position);
            timer = 0f;
        }
    }

    private void UpdatePatrol()
    {

        if (distanceToPlayer < aggroRange)
        {
            State = States.Chase;
            return;
        }

        if (distanceToWayPoint <= agent.stoppingDistance)
        {
            State = States.Idle;
            return;
        }


    }

    private void UpdateIdle()
    {
        timer += Time.deltaTime;

        if (distanceToPlayer < aggroRange)
        {
            State = States.Chase;
            return;
        }
        if (timer > idleTime)
        {
            State = States.Patrol;
            return;
        }
    }
}

