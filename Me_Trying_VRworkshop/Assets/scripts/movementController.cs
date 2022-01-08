using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class movementController : MonoBehaviour
{
    public XRNode inputSourceFromDevice;

    private CharacterController mycharacter;
    public List<InputDevice> devices =new List<InputDevice>();
    private Vector2 input2DAxis;
    private Vector3 direction;
    float fspeed = 2;
    XRRig myXRRig;
    // Start is called before the first frame update
    void Start()
    {

        mycharacter = GetComponent<CharacterController>();
        InputDevices.GetDevices(devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name);
        }
        Debug.Log(mycharacter.center);
         myXRRig = GetComponent<XRRig>();
    }
    private void FixedUpdate()
    {
        Quaternion hmdyaw = Quaternion.Euler(0,myXRRig.cameraGameObject.transform.eulerAngles.y,0);
        direction = hmdyaw * new Vector3(input2DAxis.x, 0f, input2DAxis.y);

        mycharacter.Move(hmdyaw * direction * Time.fixedDeltaTime * fspeed);
    }
    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSourceFromDevice);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out input2DAxis);
    }
}
