using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrapNew : MonoBehaviour
{
    EnemyNew slowAgent;

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
        if (other.gameObject.tag == "enemy")
        {
            slowAgent = other.gameObject.GetComponent<EnemyNew>();

            slowAgent.speed = 2.5f;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            slowAgent = other.gameObject.GetComponent<EnemyNew>();

            slowAgent.speed = 5;

        }
    }
}
