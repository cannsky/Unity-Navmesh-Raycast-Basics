using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;

    public bool isDead;
    public bool isReturn;

    // Start is called before the first frame update
    void Start() => StartCoroutine(NPCBehaviour());

    public IEnumerator NPCBehaviour()
    {
        yield return new WaitForSeconds(0.1f);
        NpcBehave();
        if (!isDead) StartCoroutine(NPCBehaviour());
        else StopAI();
    }
    public void NpcBehave()
    {
        CheckIsArrived();
        HandleMovement();
    }

    public void CheckIsArrived()
    {
        if (transform.position.x <= 12f && transform.position.z <= 12f) isReturn = true;
        else if (transform.position.x >= 90f && transform.position.z >= 40f) isReturn = false;
    }

    public void HandleMovement()
    {
        if (!isReturn) SetLocation(new Vector3(10f, 0f, 10f));
        else SetLocation(new Vector3(93f, 0f, 43f));
    }

    public void SetLocation(Vector3 location)
    {
        if(!isDead) agent.SetDestination(location);
    }

    public void StopAI()
    {
        agent.enabled = false;
    }

    public void Die()
    {
        isDead = true;
        StartCoroutine(DestroyObject());
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
