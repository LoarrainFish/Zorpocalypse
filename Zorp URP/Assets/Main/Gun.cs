using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Vector3 direction;
    public float rayLength;
    public float damage = 10f;
    public float range = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            Debug.Log("Shot");
            
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        direction = Input.mousePosition;
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.DrawRay(this.transform.position, this.transform.forward * 100f, Color.red, duration: 2f);
            Debug.Log(hit.transform.name);
        }
    }
}
