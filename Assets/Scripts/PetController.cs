using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PetController : MonoBehaviour
{
    WayPointController wayPointController;
    ARPlane arplane;
    

    private void Start()
    {
        wayPointController = GameObject.Find("WayPointController").GetComponent<WayPointController>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found waypoint");
        wayPointController.SetNextWaypoint();
    }
}
