using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{
    public SteamVR_ActionSet m_ActionSet;

    public SteamVR_Action_Boolean m_BooleanAction;
    public SteamVR_Action_Vector2 m_TouchPosition;

    private void Awake()
    {
        m_BooleanAction = SteamVR_Actions._default.GrabPinch;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        #region Boolean Action

        if (m_BooleanAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("1");
        }
        if (m_BooleanAction[SteamVR_Input_Sources.LeftHand].stateDown)
        {
            print("2");
        }
        if (m_BooleanAction.stateDown)
        {
            print("3");
        }
        if (SteamVR_Actions._default.Teleport.GetStateUp(SteamVR_Input_Sources.Any))
        {
            print("4");
        }
        #endregion

        #region Vector2 Action

        Vector2 delta = m_TouchPosition[SteamVR_Input_Sources.RightHand].delta;

        #endregion
    }

    private void BoolTest(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {

    }

    private void AxisTest(ISteamVR_Action_Vector2 action, SteamVR_Input_Sources source, Vector2 axis, Vector2 delta)
    {

    }
}
