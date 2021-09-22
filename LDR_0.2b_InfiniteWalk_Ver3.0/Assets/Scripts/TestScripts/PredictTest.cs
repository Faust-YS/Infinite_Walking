using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictTest : MonoBehaviour
{
    private bool virtualObstacle = false;
    private float MAX_X = 2, MAX_Z = 2;
    private Renderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (virtualObstacle == true && OutOfTrackSpace() == true)
        {
            //print("Both");
            m_Renderer.material.color = Color.gray;

        }
        else if (virtualObstacle == true)
        {
            //print("Virtual");
            m_Renderer.material.color = Color.blue;
        }
        else if (OutOfTrackSpace() == true)
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
        if (Mathf.Abs(this.transform.position.x) > MAX_X || Mathf.Abs(this.transform.position.z) > MAX_Z)
        {
            return true;
        }
        else return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
        {
            virtualObstacle = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
        {
            virtualObstacle = false;
        }
    }
}
