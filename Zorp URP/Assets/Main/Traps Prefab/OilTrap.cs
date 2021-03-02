using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrap : MonoBehaviour
{
    ZorpMove OilAgent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            OilAgent = other.gameObject.GetComponent<ZorpMove>();

            OilAgent.isOiled = true;

        }
    }

}
