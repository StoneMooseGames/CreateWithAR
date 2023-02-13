using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Button startButton;
    public Button setPetActiveButton;
    public Button setRouteActiveButton;
    WayPointController wayPointController;

    // Start is called before the first frame update
    void Start()
    {
        wayPointController = GameObject.Find("WayPointController").GetComponent<WayPointController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
