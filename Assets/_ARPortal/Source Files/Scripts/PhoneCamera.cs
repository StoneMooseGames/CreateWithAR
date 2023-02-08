using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCamera : MonoBehaviour
{
    
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();

        if (devices.Length > 0)
        {
            for(int i=0; i < devices.Length; i++)
            {
                Debug.Log("found camera: " + devices[i].name);
            }
           
            webcamTexture.deviceName = devices[0].name;
            webcamTexture.Play();
        }
    }
}
