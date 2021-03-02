using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ThirdPersonCharacterController : MonoBehaviour
{

    public float Speed;
    public float dashTime = 2;
    public float DashSpeed;
    public float jumpSpeed = 5;
    private Rigidbody rb;
    private bool onGround = true;
    private const int MAX_JUMP = 2;
    private int currentJump = 0;
    private bool DashOnCooldown = false;
    public float DashCooldownTime = 6;
    
    //public GameObject stealthVisualEffect;
    private bool isSprinting = false;
    public float Stamina = 6;
    //public GameObject dashIcon;
    //public GameObject focusVisionIcon;
    //public GameObject sprintIcon;

    public bool StealthActive = false;
    bool walkingBackwards = false;
    private bool applyJumpForce = false;

    public GameObject cam;



    public float Vison = 6;
    public bool visonCooldown = false;
    private Vector3 playerMovement;

    public GameObject playerShot;
    public bool playerHit;
    public AudioSource Alert;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
       // dashIcon.SetActive(true);
        //focusVisionIcon.SetActive(true);
       // sprintIcon.SetActive(true);

    }

    public void StopDashVelocity()
    {
        rb.velocity = Vector3.zero;
        Debug.Log("Dashed");
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHit == true)
        {
            //playerShot.SetActive(true);
        }

        //if (GameManager.isPaused)
        //{
        //    return;
        //}

        PlayerMovement();
        Animations();

        if (Input.GetKeyDown("e") && Input.GetKey("w"))
        {
            Debug.Log("keydown");


            if (DashOnCooldown == false && onGround == true)
            {
                rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
                DashCooldownTime = 6;
                Invoke("StopDashVelocity", dashTime);

                Debug.Log("Dashed");
                //animator.SetTrigger("Dash");
                //dashIcon.SetActive(false);

            }
        }

        if (Input.GetKeyDown("e") && Input.GetKey("s"))
        {
            Debug.Log("keydown");


            if (DashOnCooldown == false && onGround == true)
            {
                rb.AddForce(-transform.forward * DashSpeed, ForceMode.Impulse);
                DashCooldownTime = 6;
                Invoke("StopDashVelocity", dashTime);

                Debug.Log("Dashed");
                //animator.SetTrigger("Dash");
                //dashIcon.SetActive(false);
            }
        }



        if (Input.GetKeyDown(KeyCode.LeftShift) && (Stamina > 0))
        {
            Speed = 12;
            isSprinting = true;
            //sprintIcon.SetActive(false);

           
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 7;
            isSprinting = false;

           
        }

        if (DashCooldownTime <= 0)
        {
            DashOnCooldown = false;
            //dashIcon.SetActive(true);
        }
        else
        {
            DashCooldownTime -= Time.deltaTime;
            DashOnCooldown = true;
        }

        if (isSprinting == true)
        {
            Stamina -= Time.deltaTime;

        }
        else
        {
            Stamina += Time.deltaTime;
        }

        if (Stamina > 6)
        {
            Stamina = 6;
            //sprintIcon.SetActive(true);
        }

        if (Stamina < 0)
        {
            Speed = 7;
            isSprinting = false;
            //sprintIcon.SetActive(true);
        }



        // print(DashCooldownTime);

        if (Input.GetKeyDown(KeyCode.C))
        {
            StealthActive = !StealthActive;
           // if (stealthVisualEffect != null)
               // stealthVisualEffect.SetActive(StealthActive);
        }


        if (StealthActive == true)
        {
            Speed = 2;
        }
        else if (walkingBackwards)
        {
            Speed = 4;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            Speed = 7;
        }

        CheckForJump();


        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (Vison <= 0)
            {
                visonCooldown = false;
                //focusVisionIcon.SetActive(false);
            }
            else visonCooldown = true;


            if (visonCooldown == false)
            {
                Invoke("visionWork", 0);
                Vison = 6;
            }





        }

        if (Vison <= 0)
        {
            //focusVisionIcon.SetActive(true);




        }
        else
        {
            Vison -= Time.deltaTime;
        }

    }


    void visionWork()
    {
        cam.active = !cam.active;

        Invoke("visionOff", 3);
    }

    void visionOff()
    {
        cam.active = !cam.active;

    }

    void CheckForJump()
    {
        if (Input.GetKeyDown("space") && (onGround || MAX_JUMP > currentJump))
        {
            applyJumpForce = true;
            onGround = false;

            if (currentJump < MAX_JUMP && currentJump > 0)
            {
                
            }

            currentJump++;

        }
        if (currentJump <= 1)
        {
            
        }
    }

    private void FixedUpdate()
    {
        if (applyJumpForce)
        {
            applyJumpForce = false;
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        if (ver < 0) walkingBackwards = true;
        else walkingBackwards = false;
        playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);

    }

    void Animations()
    {


        bool isMoving = (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0);





        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                onGround = true;
                currentJump = 0;
            }
        }


    }
}
