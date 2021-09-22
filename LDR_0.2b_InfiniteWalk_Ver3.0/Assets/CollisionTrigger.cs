using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private Renderer m_Renderer;
    private static bool lineCollider;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        lineCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "LineRenderer" || other.name == "NextPosition")
        {
            print("--------------------------Collider " + other.name);
            m_Renderer.material.color = Color.green;
            lineCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "LineRenderer" || other.name == "NextPosition")
        {
            m_Renderer.material.color = Color.black;
        }
    }
    public static bool GetLineCollider()
    {
        return lineCollider;
    }
}
