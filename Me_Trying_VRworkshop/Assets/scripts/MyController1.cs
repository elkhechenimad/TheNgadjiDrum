///////////////////////////////////////////////////////////////
///
///  XR Interaction Toolkit Wokshop, ER-P3
///  Prof. Dr. Frank Gabler 
///  01.12.21
/// 
///////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
// we need to add this to get access to the VR hardware
using UnityEngine.XR;

public class MyController1 : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    // to store the device we want to read/use
    private InputDevice targetDevice;
    // create a list to store all the different controller models and pick them dynamically 
    public List<GameObject> controllerPrefabs;
    // store the reference to the prefab of the controller instantiated 
    private GameObject myController;
    // Start is called before the first frame update
    void InitializeController()
    {
        List<InputDevice> attached_devices = new List<InputDevice>();

        // Set the search bit-mask: this will give you only the right controller:
        //   controllerCharacteristics = (InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right);
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, attached_devices);

        // loop over the list and show the name and the characteristics of the device
        foreach (var item in attached_devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        // check first if there are any entries in the list of devices
        if (attached_devices.Count > 0)
        {
            // grab the first device from the list and store it
            targetDevice = attached_devices[0];

            // search for a fitting name. Watchout! Must exactly fit to the name of the attached controller ( targetDevice.name)
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                myController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Error: Controller prefab not found!");
                // not found, just pick the first one in the list
                myController = Instantiate(controllerPrefabs[0], transform);
            }

        }
    }

    void Start()
    {
        // if you would try to read the list of devices here, it might be to early.  
    }

    // Update is called once per frame
    void Update()
    {
        // check if device was already selected 
        if (!targetDevice.isValid)
        {
            InitializeController();
        }

        // if statement to check if feature is supported by the controller used
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primarayButtonValue))
        {
            Debug.Log("Primary Button was pressed");
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)
            && (triggerValue > 0.1f))
        {
            Debug.Log("Trigger value: " + triggerValue);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)
            && (gripValue > 0.1f))
        {
            Debug.Log("Grip value: " + gripValue);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 Primary2DAxisValue)
            && (Primary2DAxisValue != Vector2.zero))
        {
            Debug.Log("Primary 2D Axis (Touch): " + Primary2DAxisValue);
        }

    }
}
