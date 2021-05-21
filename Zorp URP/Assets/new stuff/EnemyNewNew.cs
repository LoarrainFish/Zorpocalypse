using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNewNew : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private NavMeshAgent nav;

    public float health = 100f;

    public bool Oil;
    public bool fire;



    private void Start()
    {

        target = Waypoints.points[0];
        nav = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        nav.SetDestination(dir.normalized * speed * Time.deltaTime);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.GetComponent<ExplodeZorp>().Explosion();
            Die();
        }
    }

    public void Die()
    {

        Destroy(gameObject);
    }
}
