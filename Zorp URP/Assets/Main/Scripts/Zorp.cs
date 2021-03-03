using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zorp : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    GameObject GAMEMANAGER_GameObject;
    GameManager _GameManager;
    public HealthBar healthBar;

    void Start()
    {
        GAMEMANAGER_GameObject = GameObject.FindWithTag("GameManager");
        _GameManager = GAMEMANAGER_GameObject.GetComponent<GameManager>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _GameManager.EnemiesRemaining(1);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if(currentHealth == 1)
        {
            Debug.Log("Kill");
            _GameManager.EnemiesRemaining(-1);
            Destroy(this.gameObject);
        }

        healthBar.SetHealth(currentHealth);
    }

}
