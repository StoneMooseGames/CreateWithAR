using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RouteManager : MonoBehaviour
{
    public List<Route> routeList;
    public TMP_Text currentRouteText;
    // Start is called before the first frame update
    void Start()
    {
        currentRouteText.text = "Current route: " + routeList[0].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
