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
    ARPlane arplane;
    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject anchor;

    private void Awake()
    {
        arplane = GameObject.Find("AR Session Origin").GetComponent<ARPlane>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        arRaycastManager = GetComponentInChildren<ARRaycastManager>();
        anchor = GameObject.Find("Anchor");
    }
    
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if((Input.touchCount > 0))
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
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
            //GetTouchPosition();
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
        for(int i=1;i< chosenRoute.routePoints.Count; i++)
        {
            GameObject newWaypoint =  Instantiate(waypoint, chosenRoute.routePoints[i], new Quaternion(0, 0, 0, 0));
            newWaypoint.transform.parent = anchor.transform;

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
            //GameObject.Find("StartButton").SetActive(false);
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

    void GetTouchPosition()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if(arRaycastManager.Raycast(touchPosition,hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                var hitPose = hits[0].pose;
                foreach(var plane in arPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                arPlaneManager.enabled = false;
                spawnNew = Instantiate(petChosen,anchor.transform.position, hitPose.rotation);
                //spawnNew.transform.parent = anchor.transform;
                this.GetComponent<ARRaycastManager>().enabled = false;
                
            }
        }
    }
}
