using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    WayPointController wayPointController;

    private void Start()
    {
        wayPointController = GameObject.Find("WayPointController").GetComponent<WayPointController>(); ;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found waypoint");
        wayPointController.SetNextWaypoint();
    }
}
