using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroll : MonoBehaviour
{
    [SerializeField] private GameObject [] patrolPoints;
    [SerializeField] private GameObject patrolPointsGO;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float moveNextPoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        patrolPointsGO.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > moveNextPoint)
        {
            agent.SetDestination(patrolPoints[currentPoint].transform.position);
            
            elapsedTime = 0;
            moveNextPoint = Random.Range(3, 6);
            currentPoint++;
            if (currentPoint == patrolPoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
