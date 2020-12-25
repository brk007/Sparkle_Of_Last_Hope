using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent.SetDestination(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
