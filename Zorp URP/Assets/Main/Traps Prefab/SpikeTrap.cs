using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    ZorpMove HealthAgent;
    public GameObject spikes;
    public float lastDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(lastDamage >= 1)
        {
            lastDamage = 0;
        }
        lastDamage += Time.fixedDeltaTime;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("Trigger");
            HealthAgent = other.gameObject.GetComponent<ZorpMove>();


                HealthAgent.TakeDamage(1);

            

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "enemy")
    //    {
    //        HealthAgent = other.gameObject.GetComponent<ZorpMove>();

            

    //    }
    //}

    
}
