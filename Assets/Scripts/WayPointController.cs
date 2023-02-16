using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class WayPointController : MonoBehaviour
{
    private GameObject spawnNew;
    public List<Route> routeList;
    public Petlist petList;
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
    public Vector3 nextPoint;
    ARTrackedImageManager arImageManager;
    
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject anchor;

    private void Awake()
    {
        arImageManager = GetComponent<ARTrackedImageManager>();
                
    }
    
    
    void Start()
    {
        speed = 2;
        currentRouteIndex = 0;
         
        //Debugging
        //ChoosePet(3);
        //ChooseRoute(0);
        //spawnNew = Instantiate(petChosen);
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
        else
        {
           
            isActive = false;
        }

    }

    public void ChoosePet(int petIndex)
    {
        petChosen = petList.pets[petIndex];
        spawnNew = Instantiate(petChosen, anchor.transform.position, anchor.transform.rotation);
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
        anchor = GameObject.FindGameObjectWithTag("Anchor");
        if (anchor != null)
        {
            anchor.transform.rotation = new Quaternion(0, 0, 0, 0);
            arImageManager.enabled = false;
            for (int i = 1; i < chosenRoute.routePoints.Count; i++)
            {
                GameObject newWaypoint = Instantiate(waypoint, anchor.transform.position + chosenRoute.routePoints[i], new Quaternion(0, 0, 0, 0));
                newWaypoint.transform.parent = anchor.transform;

            }
        }
        
       
        isRouteDone = true;
    }

    public void MovePet()
    {
        Rigidbody petRb = spawnNew.GetComponent<Rigidbody>();
        //Rigidbody petRb = petChosen.GetComponent<Rigidbody>();
        petRb.transform.LookAt(nextPoint);
        petRb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            
    }

    public void SetNextWaypoint()
    {
        
       
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
        petSelected = true;
        
    }

    public void ToggleRouteSelected()
    {
        routeSelected = true;
    }

    
}
