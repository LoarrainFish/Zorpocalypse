using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public Transform trapSnapLocation;
    [SerializeField]
    private Transform nearestObject;
    private List<GameObject> snapObjectsList;
    [SerializeField]
    private GameObject[] snapObjects;
    public float _offsetY = 0.5f;

    [SerializeField]
    LayerMask chosenLayers;

    public GameObject _player;
    public Vector3 _playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        TrapUI.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(transform.position, mousePos, Color.red);

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

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            TrapUI.gameObject.SetActive(false);
            HandleNewObjectHotKey(6);
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

    Transform GetClosest(Transform[] targetSnapPoints)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in targetSnapPoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


    private void MoveCurrentObjectToMouse()
    {
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            var currentPos = hitInfo.transform.position + hitInfo.normal;
            Transform[] snapLocations = hitInfo.transform.GetComponentsInChildren<Transform>();           

            float nearestDistance = float.MaxValue;
            for (int i = 0; i < snapLocations.Length; i++)
            {
                float distance = (snapLocations[i].transform.position - _player.transform.position).sqrMagnitude;
                float distanceToSnap = Vector3.Distance(snapLocations[i].transform.position, hitInfo.point);

                if (distance < nearestDistance)
                {
                    nearestObject = snapLocations[i];
                    nearestDistance = distance;
                }
            }


                

            if (hitInfo.transform.tag == "TrapAllowed")
            {
                currentPlaceableObject.transform.position = nearestObject.transform.position;
                currentPlaceableObject.transform.rotation = nearestObject.transform.rotation ;
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
