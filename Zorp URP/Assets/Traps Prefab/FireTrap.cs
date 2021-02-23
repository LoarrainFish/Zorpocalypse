using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    AI FireAgent;
    public float lastDamage;
    public float OillastDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lastDamage >= 2)
        {
            lastDamage = 0;
        }
        lastDamage += Time.fixedDeltaTime;

        if (OillastDamage >= 3)
        {
            OillastDamage = 0;
            FireAgent.Oil = false;
        }
        OillastDamage += Time.fixedDeltaTime;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            FireAgent = other.gameObject.GetComponent<AI>();

            if (other.gameObject != null && lastDamage >= 2)
            {
                FireAgent.aiHealth -= 10;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            FireAgent = other.gameObject.GetComponent<AI>();

            if(FireAgent.Oil == true && lastDamage >= 3)
            {
                FireAgent.aiHealth -= 10;
            }

        }
    }
}
