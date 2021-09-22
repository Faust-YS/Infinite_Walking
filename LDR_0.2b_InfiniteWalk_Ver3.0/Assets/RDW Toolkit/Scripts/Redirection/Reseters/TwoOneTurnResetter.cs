using UnityEngine;
using System.Collections;

/// <summary>
/// This type of reset injects a 180 rotation. It will show a prompt to the user once at the full rotation is applied and the user is roughly looking at the original direction.
/// The method is simply doubling the rotation amount. No smoothing is applied. No specific rotation is enforced this way.
/// </summary>
public class TwoOneTurnResetter : Resetter {

    ///// <summary>
    ///// The user must return to her original orientation for the reset to let go. Up to this amount of error is allowed.
    ///// </summary>
    //float MAX_ORIENTATION_RETURN_ERROR = 15;

    float overallInjectedRotation;
    private int resetTimes = 0;
    private Transform prefabHUD, instanceHUD = null;
    private int LorR; // 1=Left and -1=Right
    //private GameObject cylinder;
    private bool inReset = false;

    private void Start()
    {
        //cylinder = GameObject.Find("Cylinder");
        //cylinder.SetActive(false);
    }
    private void Update()
    {
    }
    public override bool IsResetRequired()
    {
        return !isUserFacingAwayFromWall();
    }

    public override void InitializeReset()
    {
        overallInjectedRotation = 0;
        prefabHUD = null;
        inReset = false;
        LorR = TurnWhere(FaceTo().ToString());
        SetHUD(FaceTo().ToString());
    }
    
    public override void ApplyResetting()
    {
        //Debug.Log("Apply Resetting");
        if (LorR == 1)
        {
            Debug.Log("Turn Left 1");
            if (Mathf.Abs(overallInjectedRotation) > -180)
            {
                float remainingRotation = redirectionManager.deltaDir < 0 ? 180 + overallInjectedRotation : -180 + overallInjectedRotation; // The idea is that we're gonna keep going in this direction till we reach objective

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
            }
        }
        else
        {
            Debug.Log("Turn Right -1");
            if (Mathf.Abs(overallInjectedRotation) < 180)
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
            }
        }
        /*if(!IsUserNearAndFaceToBound())
        {
            redirectionManager.OnResetEnd();
        }*/
    }

    private int TurnWhere(string bound)
    {
        if (bound == "Right" && redirectionManager.currPosReal.x > 0 && redirectionManager.currPosReal.z > 0 ||
           bound == "Bottom" && redirectionManager.currPosReal.x > 0 && redirectionManager.currPosReal.z < 0 ||
           bound == "Left" && redirectionManager.currPosReal.x < 0 && redirectionManager.currPosReal.z < 0 ||
           bound == "Top" && redirectionManager.currPosReal.x > 0 && redirectionManager.currPosReal.z < 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public override void FinalizeReset()
    {
        //Destroy(instanceHUD.gameObject);
        GameObject[] objects = GameObject.FindGameObjectsWithTag("coin");
        GameObject[] particles = GameObject.FindGameObjectsWithTag("particles");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
        foreach (GameObject particle in particles)
        {
            Destroy(particle);
        }
        //cylinder.SetActive(false);
    }

    public void SetHUD(string boundary)
    {
        /*if (prefabHUD == null)
        {
            if (boundary == "Left") prefabHUD = Resources.Load<Transform>("BumpToLeft");
            else if (boundary == "Right") prefabHUD = Resources.Load<Transform>("BumpToRight");
            else if (boundary == "Top") prefabHUD = Resources.Load<Transform>("BumpToTop");
            else if (boundary == "Bottom") prefabHUD = Resources.Load<Transform>("BumpToBottom");
        }
        instanceHUD = Instantiate(prefabHUD);
        instanceHUD.parent = redirectionManager.headTransform;
        instanceHUD.localPosition = instanceHUD.position;
        instanceHUD.localRotation = instanceHUD.rotation;*/
        if (inReset == false)
        {
            //cylinder.SetActive(true);
            //cylinder.transform.position = new Vector3(redirectionManager.headTransform.transform.position.x, -0.01f, redirectionManager.headTransform.transform.position.z);
            resetTimes++;
            print("ResetTime: " + resetTimes);
            CreateCubeAngle60();
            inReset = true;
        }
    }
    public override void SimulatedWalkerUpdate()
    {
        // Act is if there's some dummy target a meter away from you requiring you to rotate
        //redirectionManager.simulatedWalker.RotateIfNecessary(180 - overallInjectedRotation, Vector3.forward);
        redirectionManager.simulatedWalker.RotateInPlace();
        //print("overallInjectedRotation: " + overallInjectedRotation);
    }
    private void CreateCubeAngle60()
    {
        Vector3 centerPos;
        float radius = 0.5f;
        float angle = 0;
        centerPos = redirectionManager.headTransform.transform.position;

        //20度生成一个圆
        for (angle = 0; angle < 360; angle += 60)
        {
            //先解决你物体的位置的问题
            // x = 原点x + 半径 * 邻边除以斜边的比例,   邻边除以斜边的比例 = cos(弧度) , 弧度 = 角度 *3.14f / 180f;   
            float x = centerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);
            float z = centerPos.z + radius * Mathf.Sin(angle * 3.14f / 180f);
            // 生成一个圆
            Transform temp = Resources.Load<Transform>("coin");
            Transform obj1 = Instantiate(temp);
            
            //设置物体的位置Vector3三个参数分别代表x,y,z的坐标数  
            obj1.position = new Vector3(x, centerPos.y, z);
        }
    }
}
