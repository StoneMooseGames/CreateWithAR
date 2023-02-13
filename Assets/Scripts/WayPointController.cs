using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class WayPointController : MonoBehaviour
{
    public List<Route> routeList;
    public List<GameObject> petList;
    public GameObject waypoint;
    GameObject petChosen;
    Route chosenRoute;
    public bool petSelected = false;
    public bool routeSelected = false;
    public bool isActive;
    int currentRouteIndex;
    float speed;
    bool isRouteDone = false;
    Vector3 currentPoint;
    Vector3 nextPoint;
    ARPlane arplane;
    private void Awake()
    {
        arplane = GameObject.Find("AR Session Origin").GetComponent<ARPlane>();   
    }
    void Start()
    {
        speed = 2;
        currentRouteIndex = 0;
        petSelected = false;
        routeSelected = false;
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
            CheckRange();
        }
        else isActive = false;
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
        
        /*Debug.Log(currentRouteIndex);
        Debug.Log(nextPoint);
        Debug.Log(chosenRoute.routePoints.Count);*/
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

    void CheckRange()
    {
        
            RaycastHit checkForPlayer;
            // create the ray to use
            Ray ray = new Ray(transform.position, GameObject.FindGameObjectWithTag("playerCharacter").transform.position - transform.position);
            //casting a ray against the player
            if (Physics.Raycast(ray, out checkForPlayer))
            {
                //we are here if the ray hit a collider
                //now to check if that collider is the player
                if (checkForPlayer.collider.gameObject.GetComponent<ARSessionOrigin>())
                {
                Debug.Log(checkForPlayer.distance);
                Debug.DrawLine(this.transform.position,checkForPlayer.transform.position,Color.red);
                
                }
            }
            
        
    }
}
