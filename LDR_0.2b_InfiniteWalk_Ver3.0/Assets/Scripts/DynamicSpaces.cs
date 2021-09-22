using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpaces : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject firstWall,nextWall, spaces,trigger;
    void Start()
    {
        firstWall = GameObject.Find("Path (6)");
        firstWall.SetActive(true);
        nextWall = GameObject.Find("Path");
        nextWall.SetActive(false);
        spaces = GameObject.Find("DynamicSpace");
        spaces.SetActive(false);
        trigger = GameObject.Find("TriggerNext");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "nextscene")
        {
            nextWall.SetActive(true);
            firstWall.SetActive(false);
            GameObject.Find("Redirected User").transform.position -= new Vector3(0, 0, 8f);
            spaces.transform.position -= new Vector3(0, 0, 8f);
            trigger.SetActive(false);
        }
    }
}
