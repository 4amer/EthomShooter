using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private Animator enemyAnimator;
    private NavMeshAgent agent;
    private Transform player;

    [SerializeField] private float hitTimeConst;
    private float hitTime;
   
    private Rigidbody rb;
    private CapsuleCollider collider;

    [SerializeField] private int HP;

    [SerializeField] private float timeToDestroy;

    [SerializeField] private GameObject bloodEffect;
    private void Start()
    {
        hitTime = hitTimeConst;
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        agent.SetDestination(player.position);
        if (HP <= 0)
        {
            enemyAnimator.SetBool("Dead", true);
            Destroy(rb);
            Destroy(collider);
            agent.speed = 0;
        }     
        if (hitTime < 0)
        {
            enemyAnimator.SetTrigger("Hit");
            hitTime = hitTimeConst;
        }
        if (enemyAnimator.GetBool("Dead") == true)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy < 0)
            {
                Destroy(gameObject);
            }
        }
        hitTime -= Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Yes");
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Instantiate(bloodEffect, transform.position + new Vector3(0, 1, 0), transform.rotation);
            HP -= 1;
        }
    }
}
