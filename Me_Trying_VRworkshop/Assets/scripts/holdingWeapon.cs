using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class holdingWeapon : MonoBehaviour
{

  
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    Vector3 leftCtrlPosition = new Vector3(0, 0, 0);
    Vector3 rightCtrlPosition = new Vector3(0, 0, 0);
    List<InputDevice> attachedDevices;
    private InputDevice targetDevice;
    public float bulletVelocity=5;

    public GameObject bullet;
    private GameObject bulletinstance;

    // Start is called before the first frame update
    void Start()
    {
  

    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
        initialzeControllers();
        aim();

        if(attachedDevices[1].TryGetFeatureValue(CommonUsages.triggerButton,out bool triggerBtnValue))
        {
            if (triggerBtnValue)
            {
                Debug.Log(triggerBtnValue);
                shoot();
            }
            else
            {

            }
        }
      
    }


    void aim()
    {

      
        Debug.Log(transform.position);

        if (attachedDevices[0].TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 Position1))
        {
            leftCtrlPosition = Position1;
        }
        if (attachedDevices[1].TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 Position2))
        {
            rightCtrlPosition = Position2;
        }
        Vector3 weaponDirection = (leftCtrlPosition - rightCtrlPosition);
        Vector3 weaponPosition = (leftCtrlPosition + rightCtrlPosition) / 2;
        Quaternion rotation = Quaternion.LookRotation(weaponDirection);
        transform.rotation = rotation;
        transform.position = weaponPosition;
    }

    void initialzeControllers()
    {
        attachedDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, attachedDevices);
       
    }
    void shoot()
    {    
        bulletinstance=    Instantiate(bullet, transform);
        Vector3 weaponDirection = Vector3.Normalize(leftCtrlPosition - rightCtrlPosition);
        Rigidbody rigidbody= bulletinstance.GetComponent<Rigidbody>();
        
        rigidbody.AddForce(weaponDirection*bulletVelocity);
    }
}
