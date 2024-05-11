using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    Animator animator;
    NavMeshAgent agent;

    [SerializeField] float attackRange;
    [SerializeField] float sprintRange;
    float distance;

    [SerializeField] int damage;
    [SerializeField] float health;

    [SerializeField] bool isDead;

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
            player.GetComponent<Health>().AddHealth(-damage);
        }
        else
        {
            animator.ResetTrigger("Attack");
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
