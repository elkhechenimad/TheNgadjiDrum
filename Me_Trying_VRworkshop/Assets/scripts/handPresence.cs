using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class handPresence : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDeviceAtXRNode(XRNode.LeftEye);
        //  InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics,devices);
        InputDevices.GetDevices(devices);   
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        Debug.Log(devices.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
