using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float attackRange;
    [SerializeField] float sprintRange;
    [SerializeField] int damage;
    [SerializeField] float health;
    [SerializeField] float distance;

    [SerializeField] bool isDead;

    Animator animator;
    NavMeshAgent agent;

    public event Action<int> damagePlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(isDead)
        {
            return;
        }
        distance = (player.transform.position - transform.position).magnitude;
        agent.SetDestination(player.transform.position);

        if (distance < sprintRange)
        {
            agent.isStopped = false;
            animator.SetBool("Sprint", false);
        }
        else if (distance > sprintRange)
        {
            agent.isStopped = false;
            animator.SetBool("Sprint", true);

        }

        if (distance < attackRange)
        {
            agent.isStopped = true;
            animator.SetTrigger("Attack");
            return;
        }
    }

    public void OnAttackHit()
    {
        if (distance < attackRange)
        {
            damagePlayer?.Invoke(damage);
        }
        else
        {
            animator.ResetTrigger("Attak");
            agent.isStopped = false;
        }
    }

    public void DealDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            isDead = true;
            agent.isStopped = true;
            animator.SetTrigger("Dead");
        }
    }
}
