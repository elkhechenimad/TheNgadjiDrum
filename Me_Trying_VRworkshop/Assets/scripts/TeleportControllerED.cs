// inspired by VALEM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportControllerED : MonoBehaviour
{
    // access to our teleportation rays:
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    // Which button activates the teleport?
    public InputHelpers.Button teleportActivationButton;
    // how much is needed (e.g. trigger value)
    public float activationThreshold = 0.2f;
    
    // get/set to link it to an event
    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    // Update is called once per frame
    void Update()
    {
        if(leftTeleportRay)
        {
            leftTeleportRay.GetComponent<XRRayInteractor>().enabled = EnableLeftTeleport && CheckIfActivated(leftTeleportRay);
        }

        if (rightTeleportRay)
        {
            rightTeleportRay.GetComponent<XRRayInteractor>().enabled = EnableRightTeleport && CheckIfActivated(rightTeleportRay);
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        // check if a specific button (controller feature) was pressed:
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
