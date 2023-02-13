using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    public List<Route> routeList;
    public List<GameObject> petList;
    public GameObject waypoint;
    GameObject petChosen;
    Route chosenRoute;
    public bool petSelected;
    public bool routeSelected;
    public bool isActive;
    int currentRouteIndex;
    float speed;
    bool isRouteDone = false;
    Vector3 currentPoint;
    Vector3 nextPoint;

    void Start()
    {
        speed = 2;
        currentRouteIndex = 0;
        
        ChoosePet(0);
        ChooseRoute(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && routeSelected && petSelected)
        {
            if (currentRouteIndex == chosenRoute.routePoints.Count - 1) return;

            currentPoint = chosenRoute.routePoints[currentRouteIndex];
            nextPoint = chosenRoute.routePoints[currentRouteIndex + 1];
            MovePet();
        }
    }

    public void ChoosePet(int petIndex)
    {
        petChosen = petList[petIndex];
        petSelected = true;
            
    }

    public void ChooseRoute(int routeIndex)
    {
        chosenRoute = routeList[routeIndex];
        routeSelected = true;
        CreateWayPoints();
    }

    void CreateWayPoints()
    {
        for(int i=1;i< chosenRoute.routePoints.Count; i++)
        {
            Instantiate(waypoint, chosenRoute.routePoints[i], new Quaternion(0, 0, 0, 0));
        }          
        
        isRouteDone = true;
    }

    public void MovePet()
    {
        Rigidbody petRb = petChosen.GetComponent<Rigidbody>();
        petRb.transform.LookAt(nextPoint);
        petRb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            
    }

    public void SetNextWaypoint()
    {
        
        Debug.Log(currentRouteIndex);
        Debug.Log(nextPoint);
        Debug.Log(chosenRoute.routePoints.Count);
        currentRouteIndex++;
        if(currentRouteIndex == chosenRoute.routePoints.Count - 1)
        {
            isActive = false;
        }
    }

    public void SetActive()
    {
        if(petSelected && routeSelected)
        {
            isActive = true;
        }
       
    }
    public void TogglePetSelected()
    {
        petSelected = !petSelected;
    }

    public void ToggleRouteSelected()
    {
        routeSelected = !routeSelected;
    }
}
