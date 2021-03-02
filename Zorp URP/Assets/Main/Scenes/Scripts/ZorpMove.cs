using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZorpMove : MonoBehaviour
{
    [SerializeField]
    GameObject _destination;

    HealthBar _healthBar;

    NavMeshAgent _navMeshAgent;
    public GameObject Zorp;
    private int destPoint = 0;
    public Transform[] patrolWayPoints;
    public int currentPatrolInt = 0;
    float distance;

    public bool isOiled;

    public int _health = 20;

    // Start is called before the first frame update
    void Start()
    {
        Zorp = this.gameObject;
        _navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _healthBar = this.gameObject.GetComponent<HealthBar>();
        _destination = GameObject.FindGameObjectWithTag("Finish");
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nax mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            _healthBar.SetMaxHealth(_health);
            SetDestination();
        }



    }

    private void Update()
    {

        if (_health == 0)
        {
            Destroy(this.gameObject);
        }

        if (isOiled)
        {
            //_navMeshAgent.speed = 2.5f;
        }

        //Debug.Log("Distance to next: " + distance + "  Waypoint:" + currentPatrolInt);

    }


    private void SetDestination()
    {
        _navMeshAgent.destination = _destination.transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "kill")
        {
            Destroy(this.gameObject);

        }
    }




    public void TakeDamage(int health)
    {

            int tempHealth = _health - health;
            _healthBar.SetHealth(tempHealth);
            _health = tempHealth;


    }
}
