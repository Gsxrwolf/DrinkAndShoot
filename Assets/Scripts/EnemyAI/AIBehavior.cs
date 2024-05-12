using Player;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class AIBehavior : MonoBehaviour
{
    public GameObject player;
    public EnemyPoolSpawner spawner;
    Animator animator;
    NavMeshAgent agent;


    [SerializeField] float attackRange;
    [SerializeField] float sprintRange;
    float distance;

    [SerializeField] int damage;
    [SerializeField] float health;

    [SerializeField] bool isDead;

    [SerializeField] GameObject[] itemDrops;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        distance = (player.transform.position - transform.position).magnitude;

        if (distance < sprintRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            animator.SetBool("Sprint", false);
        }
        else if (distance > sprintRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
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

    public void DespawnEnemy()
    {
        SpawnRandomDrop();

        spawner.DespawnEnemy(this.gameObject);
    }

    private void SpawnRandomDrop()
    {
        System.Random rnd = new System.Random();

        GameObject drop;

        drop = itemDrops[rnd.Next(itemDrops.Length)];

        Instantiate(drop, transform.position, transform.rotation);
    }
}
