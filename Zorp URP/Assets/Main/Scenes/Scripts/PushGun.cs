using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushGun : MonoBehaviour
{
    // Start is called before the first frame update

    public float chargeRate;
    public float pushAmount;
    private float pushStart;
    public float pushRadius;
    public float radiusStart;
    


    public void Start()
    {
        pushStart = pushAmount;
        radiusStart = pushRadius;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            pushAmount += chargeRate;
            pushRadius += chargeRate;
            
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            DoPush();
        }
    }

    public void DoPush()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius);

        foreach (Collider pushedObject in colliders)
        {
            if (pushedObject.CompareTag("enemy"))
            {
                Rigidbody pushedBody = pushedObject.GetComponent<Rigidbody>();
                

                pushedBody.AddExplosionForce(pushAmount, Vector3.up, pushRadius);

                
            }
            

        }

        pushAmount = pushStart;
        pushRadius = radiusStart;
        
    }

}
