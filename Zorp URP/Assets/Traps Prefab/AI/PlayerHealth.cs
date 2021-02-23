using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour {

    public float health = 100f;
    public float deathDelay = 2;
    public bool isDead = false;
    
    public ThirdPersonCharacterController cc;
    public Rigidbody rb;
    public void Update()
    {
        if(health <= 0 && isDead == false)
        {
            Debug.Log("Die");
            Invoke("Die", deathDelay);
            
            isDead = true;
            //cc.enabled = false;
            rb.isKinematic = true;
        }
    }

    
}
