using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowTrap : MonoBehaviour
{

    AI slowAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            slowAgent = other.gameObject.GetComponent<AI>();

            slowAgent.agent.speed = 1;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            slowAgent = other.gameObject.GetComponent<AI>();

            slowAgent.agent.speed = 4;

        }
    }

}
