using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyClass : Character
{   
    public EnemyState enemyState;
    public float attackDst;

    public NavMeshAgent agent;
    public Transform player;

    public override void CustomStart()
    {
        player = FindAnyObjectByType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        StateHandler();
        ControllEnemy();
    }

    public virtual void ControllEnemy()
    {
        switch (enemyState)
        {
            case EnemyState.Chase:
                agent.SetDestination(player.position);
            break;
            case EnemyState.Attack:
                agent.SetDestination(transform.position);
                if (canAttack) Attack();
            break;
        }
    }

    private void StateHandler()
    {
        if (Vector3.Distance(transform.position, player.position) > attackDst)
        {
            enemyState = EnemyState.Chase;
        }
        else
        {
            enemyState = EnemyState.Attack;
        }
    }
}

public enum EnemyState
{
    Chase,
    Attack
}
