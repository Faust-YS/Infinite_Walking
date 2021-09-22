using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaveAwayResetter : Resetter
{
    ///// <summary>
    ///// The user must return to her original orientation for the reset to let go. Up to this amount of error is allowed.
    ///// </summary>
    //float MAX_ORIENTATION_RETURN_ERROR = 15;

    float overallInjectedRotation;

    private Transform prefabHUD = null;
    private string nowBoundary = null;

    Transform instanceHUD;

    public override bool IsResetRequired()
    {
        return !isUserFacingAwayFromWall();
    }

    public override void InitializeReset()
    {
        //overallInjectedRotation = 0;
        GetBoundary();
        //SetHUD(GetBoundary());
    }

    private void StopAndTurnAway(Vector3 currPosReal, string faceTo)
    {
        /*if (currPosReal.x > maxX || currPosReal.z > maxZ)
        {
            
        }*/
    }

    public override void ApplyResetting()
    {
        /*My Resetter*/
        /*if(redirectionManager.FaceTo() != nowBoundary && nowBoundary != null)
        {
            redirectionManager.OnResetEnd();
        }*/
        Debug.Log("Apply Resetting");
        /*if (Mathf.Abs(overallInjectedRotation) < 180)
        {
            float remainingRotation = redirectionManager.deltaDir > 0 ? 180 - overallInjectedRotation : -180 - overallInjectedRotation; // The idea is that we're gonna keep going in this direction till we reach objective
            if (Mathf.Abs(remainingRotation) < Mathf.Abs(redirectionManager.deltaDir))
            {
                InjectRotation(remainingRotation);
                redirectionManager.OnResetEnd();
                overallInjectedRotation += remainingRotation;
            }
            else
            {
                InjectRotation(redirectionManager.deltaDir);
                overallInjectedRotation += redirectionManager.deltaDir;
            }
        }*/
    }
    public override void FinalizeReset()
    {
        Destroy(instanceHUD.gameObject);
    }

    public void SetHUD(string boundary)
    {
        nowBoundary = boundary;
        if (prefabHUD == null)
        {
            if (boundary == "Left") prefabHUD = Resources.Load<Transform>("BumpToLeft");
            else if (boundary == "Right") prefabHUD = Resources.Load<Transform>("BumpToRight");
            else if (boundary == "Top") prefabHUD = Resources.Load<Transform>("BumpToTop");
            else if (boundary == "Bottom") prefabHUD = Resources.Load<Transform>("BumpToBottom");
            else prefabHUD = Resources.Load<Transform>("TwoOneTurnResetter HUD");
        }
        instanceHUD = Instantiate(prefabHUD);
        instanceHUD.parent = redirectionManager.headTransform;
        instanceHUD.localPosition = instanceHUD.position;
        instanceHUD.localRotation = instanceHUD.rotation;
    }
    public override void SimulatedWalkerUpdate()
    {
        // Act is if there's some dummy target a meter away from you requiring you to rotate
        //redirectionManager.simulatedWalker.RotateIfNecessary(180 - overallInjectedRotation, Vector3.forward);
        redirectionManager.simulatedWalker.RotateInPlace();
        //print("overallInjectedRotation: " + overallInjectedRotation);
    }
}
