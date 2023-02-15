using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PetController : MonoBehaviour
{
    WayPointController wayPointController;

    private void Awake()
    {
        wayPointController = GameObject.Find("AR Session Origin").GetComponent<WayPointController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found waypoint");
        wayPointController.SetNextWaypoint();
    }
}
