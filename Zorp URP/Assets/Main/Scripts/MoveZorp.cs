using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveZorp : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 ZorpTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(ZorpTarget);

        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Finish")
        {
            Destroy(other.gameObject);
            Debug.Log("-1 HP");
        }
    }
}
