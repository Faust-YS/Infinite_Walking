using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAround : MonoBehaviour
{
    private Vector3 centerPos;    //你围绕那个点 就用谁的角度
    private float radius = 3;     //物理离 centerPos的距离
    private float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreateCubeAngle30();
    }

    // Update is called once per frame
    public void CreateCubeAngle30()
    {
        centerPos = this.transform.position;
        print(centerPos);
        //20度生成一个圆
        for (angle = 0; angle < 360; angle += 20)
        {
            //先解决你物体的位置的问题
            // x = 原点x + 半径 * 邻边除以斜边的比例,   邻边除以斜边的比例 = cos(弧度) , 弧度 = 角度 *3.14f / 180f;   
            float x = centerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);
            float z = centerPos.z + radius * Mathf.Sin(angle * 3.14f / 180f);
            // 生成一个圆
            Transform temp = Resources.Load<Transform>("FlashLight");
            Transform obj1 = Instantiate(temp);
            obj1.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //设置物体的位置Vector3三个参数分别代表x,y,z的坐标数  
            obj1.position = new Vector3(x, centerPos.y, z);
        }
    }

}
