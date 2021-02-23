using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{

    public GameObject spikes;

   

    private void OnTriggerEnter(Collider other)
    {

            transform.localPosition = new Vector3(1, 0, 0);
            Debug.Log("Moved");

    }



}
