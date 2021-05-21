using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapNew : MonoBehaviour
{
    EnemyNew FireAgent;
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


        OillastDamage += Time.fixedDeltaTime;

        if (FireAgent.Oil == true && FireAgent.fire == true)
        {
            FireAgent.health -= 1;
            StartCoroutine(Burn());
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {


            FireAgent = other.gameObject.GetComponent<EnemyNew>();
            FireAgent.fire = true;
            if (other.gameObject != null && lastDamage <= 1)
            {
                FireAgent.health -= 1;
            }

        }
    }



    public IEnumerator Burn()
    {

        yield return new WaitForSeconds(2);
        FireAgent.Oil = false;
        FireAgent.fire = false;
        print("Burn");

    }
}
