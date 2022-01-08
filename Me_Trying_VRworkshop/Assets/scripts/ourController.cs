using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ourController : MonoBehaviour
{
    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefab;
    private GameObject myController;

    public GameObject handPrefab;
    private GameObject hand;

    private bool showController = true;

    private Animator handAnimator;


    void initializeController()
    {
        List<InputDevice> attachedDevices = new List<InputDevice>();
        //controllerCharacteristics = (InputDeviceCharacteristics.Controller);
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, attachedDevices);
        foreach (var item in attachedDevices)
        {
            Debug.Log("HIhihi  "+ item.name + item.characteristics);
        }
        if (attachedDevices.Count > 0)
        {
            targetDevice = attachedDevices[0];
        }


        GameObject prefab = controllerPrefab.Find(controller => controller.name == targetDevice.name);
        if (prefab)
        {
           // myController= Instantiate(prefab, transform);
        }
        else
        {
            Debug.Log("Warning: controller model not found");
        }


    }
    // Start is called before the first frame update
    void Start()
    {

        if (handPrefab)
        {
            hand = Instantiate(handPrefab, transform);
            handAnimator = hand.GetComponent<Animator>();
        }
        else
        {
            Debug.Log("Warning: handPrefab not found!");
        }
    }
    void ShowControllerfn (bool show)
    {
        if (show)
        {
           hand.SetActive(false);
           myController.SetActive(true);
        }

        else 
        {
           hand.SetActive(true);
           myController.SetActive(false);
        }
    }



 

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid){
            initializeController();
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("valTrigger", triggerValue);
        }
        else
        {
            triggerValue = 0;
        }
        if(targetDevice.TryGetFeatureValue(CommonUsages.grip,out float gripValue))
        {
      
            handAnimator.SetFloat("valGrip", gripValue);
        }
        else
        {
            gripValue = 0;
        }

        /*if (gripValue==1)
        {
            showController = false;
            
            ShowControllerfn (showController);
        }
        else
        {
            showController = true;
            ShowControllerfn(showController);
        }*/

    }
}
