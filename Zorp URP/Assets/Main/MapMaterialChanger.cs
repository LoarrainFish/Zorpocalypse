using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaterialChanger : MonoBehaviour
{
    public GameObject[] PlaceableWalls;
    public GameObject[] NonPlaceableWalls;

    public Material Good;
    public Material Bad;

    private void Start()
    {
        PlaceableWalls = GameObject.FindGameObjectsWithTag("TrapAllowed");
        NonPlaceableWalls = GameObject.FindGameObjectsWithTag("TrapNotAllowed");

        ChangeMaterials();
    }

    void ChangeMaterials()
    {
        for (int i = 0; i < PlaceableWalls.Length; i++)
        {
            PlaceableWalls[i].GetComponent<Material>().color = Color.red;
        }

    }


}
