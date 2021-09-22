using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePosition : MonoBehaviour
{
    public GameObject nextPosition;
    public Camera mainCamera;
    public GameObject Line;
    private Vector3 newPosition;
    private List<Vector3> positionList;
    private LineRenderer lineRenderer;
    private MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = Line.GetComponent<LineRenderer>();
        meshCollider = Line.AddComponent<MeshCollider>();
        positionList = new List<Vector3>();
        StartCoroutine("UpdatePosition");
    }
    // Update is called once per frame
    void Update()
    {
        //print("0: " + positionList[0] + " /1: " + positionList[1] + " /2: " + positionList[2] + " /3: " + positionList[3] + " /4: " + positionList[4]);
        nextPosition.transform.position = CalculatePosition();
        //print("PredictPosition: " + CalculatePosition() + " CameraVelocity: " + mainCamera.velocity);
        DrawFutureLine();
    }
    private IEnumerator UpdatePosition()
    {
        while (true) 
        {
            newPosition = this.transform.position;
            newPosition.y = 0;
            positionList.Add(newPosition);
            if (positionList.Count > 5)
            {
                positionList.RemoveAt(0);
            }
            yield return new WaitForSeconds(2);
        }
    }
    public void DrawFutureLine()
    {
        List<Vector3> pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1.0f / 3)
        {
            Vector3 tangentLineVertex1 = Vector3.Lerp(positionList[2], positionList[4], ratio);
            Vector3 tangentLineVectex2 = Vector3.Lerp(positionList[4], nextPosition.transform.position, ratio);
            Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
            pointList.Add(bezierPoint);
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }

    public Vector3 CalculatePosition()
    {
        return positionList[4] + new Vector3(mainCamera.velocity.x,0,mainCamera.velocity.z) * Time.deltaTime * 200;
    }
}
