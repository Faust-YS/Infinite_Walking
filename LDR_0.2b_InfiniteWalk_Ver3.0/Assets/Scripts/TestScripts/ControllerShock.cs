using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerShock : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;
    public SteamVR_Action_Boolean trackpadAction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trackpadAction.GetLastStateDown(SteamVR_Input_Sources.RightHand))
        {
            Pulse(1, 150, 75, SteamVR_Input_Sources.RightHand);
        }

        if (trackpadAction.GetLastStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Pulse(1, 150, 75, SteamVR_Input_Sources.LeftHand);
        }
    }


    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
        print("Pluse");
    }
}
