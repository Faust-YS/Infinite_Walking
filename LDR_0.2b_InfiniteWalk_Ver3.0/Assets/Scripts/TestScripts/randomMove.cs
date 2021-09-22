using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class randomMove : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    
    public float timerForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(Dosomething());
        }
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator Dosomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timerForNewPath);
        GetNewPath();

        validPath = navMeshAgent.CalculatePath(target, path);
        if (!validPath) Debug.Log("Found an invalid Path.");

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
    // Update is called once per frame
    
}
