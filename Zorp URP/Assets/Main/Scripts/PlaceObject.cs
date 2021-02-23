using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    [SerializeField]
    private KeyCode openTrapMenu = KeyCode.F;

    [SerializeField]
    private KeyCode rotateTrapLeftKey = KeyCode.Q;

    [SerializeField] 
    private KeyCode rotateTrapRightKey = KeyCode.E;

    private GameObject currentPlaceableObject;
    private int currentPrefabIndex = -1;

    public Canvas TrapUI;
    private bool trapUIOpen;

    [SerializeField]
    LayerMask chosenLayers;

    // Start is called before the first frame update
    void Start()
    {
        TrapUI.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openTrapMenu))
        {
            TrapUI.gameObject.SetActive(true);
            trapUIOpen = true;
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(0);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(1);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
        }

        if (trapUIOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TrapUI.gameObject.SetActive(false);
                trapUIOpen = false;
            }
        }


        if (currentPlaceableObject)
        {
            MoveCurrentObjectToMouse();
            ReleaseIfClicked();
            RotateTrap();        
        }



    }

    private void RotateTrap()
    {
        if (Input.GetKeyDown(rotateTrapLeftKey))
        {
            float _tempVec = currentPlaceableObject.transform.position.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, 90f);
            Debug.Log("Left");

        }
        if (Input.GetKeyDown(rotateTrapRightKey))
        {

            float _tempVec = currentPlaceableObject.transform.position.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, -90f);
            Debug.Log("Right");
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }

    }

    private void MoveCurrentObjectToMouse()
    {
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            var currentPos = hitInfo.point;


            if (hitInfo.transform.tag == "TrapAllowed")
            {
                currentPlaceableObject.transform.position = hitInfo.point;
            }

        }
    }

    private void HandleNewObjectHotKey(int requestedTrapIndex)
    {
        currentPlaceableObject = Instantiate(placeableObjectPrefabs[requestedTrapIndex]);
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }
}
