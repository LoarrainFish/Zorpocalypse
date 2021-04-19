using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    bool UIEnabled;
    public Canvas TrapUIEditor;

    // Start is called before the first frame update
    void Start()
    {
        TrapUIEditor.gameObject.SetActive(false);
        UIEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIEnabled)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Pressed L ");
                TrapUIEditor.gameObject.SetActive(true);
                LockMouse._UnlockMouse();
                UIEnabled = true;
            }
            else
            {
                //Debug.Log("Pressed L 2 ");
                TrapUIEditor.gameObject.SetActive(false);
                LockMouse._LockMouse();
                UIEnabled = false;
            }
        }
        
    }
}
