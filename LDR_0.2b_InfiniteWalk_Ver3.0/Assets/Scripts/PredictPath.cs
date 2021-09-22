using System.Collections;
using System.Collections.Generic;
using Redirection;
using UnityEngine;

public class PredictPath : MonoBehaviour
{
    private bool virtualObstacle = false;
    private bool PhysicalBound = false;
    private Vector3 relativePos;
    private float MAX_X = 2, MAX_Z = 1.25f;
    private Renderer m_Renderer;


    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        relativePos = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        //print("PlanePos: " + Utilities.FlattenedPos3D(this.transform.position));
        relativePos = Utilities.GetRelativePosition(Utilities.FlattenedPos3D(this.transform.position), GameObject.Find("Tracked Space").transform);

        if(virtualObstacle == true && OutOfTrackSpace() == true)
        {
            //print("Both");
            m_Renderer.material.color = Color.gray;

        }
        else if(virtualObstacle == true)
        {
            //print("Virtual");
            m_Renderer.material.color = Color.blue;
        }
        else if(OutOfTrackSpace() == true)
        {
            //print("OutOf");
            m_Renderer.material.color = Color.red;
        }
        else
        {
            //print("None");
            m_Renderer.material.color = Color.white;
        }
    }
    private bool OutOfTrackSpace()
    {
        if (Mathf.Abs(relativePos.x) > MAX_X || Mathf.Abs(relativePos.z) > MAX_Z)
        {
            return true;
        }
        else return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "plane")
        {
            if (other.gameObject.tag == "boundary")
            {
                PhysicalBound = true;
            }
            else if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
            {
                virtualObstacle = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "plane")
        {
            if (other.gameObject.tag == "boundary")
            {
                PhysicalBound = false;
            }
            else if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
            {
                virtualObstacle = false;
            }
        }
    }
}