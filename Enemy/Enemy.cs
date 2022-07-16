using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    Vector3 startPosition;

    public float radius = 5f;
    public float attackRadius = 4f;

    public bool isDead;
    public bool isReturn;
    public bool isNpcDetected;
    public bool isNpcNearForAttack;

    public LayerMask npcLayerMask;

    public void Start()
    {
        startPosition = transform.position;
    }

    public void Update()
    {
        isNpcDetected = Physics.CheckSphere(transform.position, radius, npcLayerMask);
        isNpcNearForAttack = Physics.CheckSphere(transform.position, attackRadius, npcLayerMask);
        if (isNpcNearForAttack) AttackNpc();
        else if (isNpcDetected) ChaseNpc();
        else agent.SetDestination(startPosition);
    }

    public void ChaseNpc()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, npcLayerMask);
        agent.SetDestination(hitColliders[0].transform.position);
    }

    public void AttackNpc()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, npcLayerMask);
        hitColliders[0].gameObject.GetComponent<NPC>().Die();
    }
}
