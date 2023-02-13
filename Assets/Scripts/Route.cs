using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Route", menuName = "Route/new Route", order = 1)]
public class Route : ScriptableObject
{
    public List<Vector3> routePoints;

}
