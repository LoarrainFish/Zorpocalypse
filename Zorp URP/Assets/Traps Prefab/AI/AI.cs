using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AI : MonoBehaviour
{
    public float sneak = 15f;
    public float Look = 10f;
    public float shoot = 5f;
    public float fieldOfViewAngle = 110f;
    public float gunDamage = 3f;
    public float patrolSpeed = 2f;
    private float patrolTimer;
    public float patrolWaitTime = 1f;
    public Transform _goal;
    private int wayPointIndex;
    PlayerHealth pH;
    public NavMeshAgent agent;
    Transform target;   
    private Transform playerSneakPos;
    public Vector3 lastPlayerPos;
    private fov fieldView;
   // public Image spotWhite;
   // public Image spotOrange;
    //public Image spotRed;
    ThirdPersonCharacterController thirdPer;
    public bool shot;

    public float aiHealth;

    public bool Oil;

    

    float distance;


    public GameObject playerOj;


    float rateOfFire;

    // Start is called before the first frame update
    void Start()
    {
        pH = playerOj.GetComponent<PlayerHealth>();        
        target = playerOj.transform;
        agent = GetComponent<NavMeshAgent>();
        thirdPer = playerOj.GetComponent<ThirdPersonCharacterController>();
        playerSneakPos = playerOj.transform;
        fieldView = GetComponent<fov>();
        aiHealth = 100;

        


    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if(aiHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (shot == true)
        {

            Invoke("aiShoot", 1);


            //spotWhite.enabled = false;
            //spotOrange.enabled = false;
            //spotRed.enabled = true;
        }
        else 


        if (shot == false)
        {
            thirdPer.playerHit = false;
        }

        if (distance <= Look && distance >= sneak && pH.health > 0)
        {

            //spotWhite.enabled = false;
            //spotOrange.enabled = true;
            //spotRed.enabled = false;
            
            fieldView.found = false;

        }
        else 


        if (distance >= Look && pH.health > 0)
        {
            //spotWhite.enabled = true;
            //spotOrange.enabled = false;
            //spotRed.enabled = false;
            fieldView.found = false;

        }


        if (distance <= sneak && pH.health > 0)
        {
            if (thirdPer.StealthActive == false)
            {
               // spotWhite.enabled = false;
                //spotOrange.enabled = false;
                //spotRed.enabled = true;
                fieldView.found = true;

                

                Debug.Log("snek");
                agent.speed = 4;
                lastPlayerPos = playerSneakPos.transform.position;
                agent.destination = lastPlayerPos;

            }
            //fieldView.found = false;

            if (thirdPer.StealthActive == true)
            {
                //spotWhite.enabled = false;
               // spotOrange.enabled = true;
                //spotRed.enabled = false;

            }




            if (!agent.pathPending && agent.remainingDistance < 0.5)
            {
                Move();
                agent.speed = 2;
            }

        }



            Move();


    }

    void aiShoot()
    {
        rateOfFire = Random.Range(0f, 1f);
        if (rateOfFire <= 0.7f)
        {
            thirdPer.playerHit = true;
            pH.health -= 1f;
        }
    }

    void Move()
    {
        agent.speed = patrolSpeed;

        agent.destination = _goal.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Look);
        Gizmos.DrawWireSphere(transform.position, sneak);
    }


}
