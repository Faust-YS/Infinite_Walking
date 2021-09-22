using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFading : MonoBehaviour
{
    public Camera cameraGameObject;
    private Transform MustStop;
    private GameObject body;
    private GameObject dynamicObstacle;
    private FindObject findObject;
   
    private int flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("Body");
        findObject = new FindObject();
        MustStop = Resources.Load<Transform>("coin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Enter the wall. " + other.gameObject.name);
            cameraGameObject.GetComponent<FadeCamera>().FadeOut();
        }
        else if(other.gameObject.tag == "boundary" && flag == 0)
        {
            print("OutOfBoundary: " + body.transform.position);
            //DynamicObstacle();
            flag = 1;
            Invoke("recoverFlag", 20);
        }
        else if(other.gameObject.tag == "randomCoin")
        {
            print("randomCoin: " + findObject.GetCoins(1));
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Exit the wall. " + other.gameObject.name);
            cameraGameObject.GetComponent<FadeCamera>().FadeIn();
        }
    }
    private void DynamicObstacle()
    {
        dynamicObstacle = Instantiate(MustStop.gameObject);
        dynamicObstacle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        dynamicObstacle.transform.position = body.transform.position + body.transform.forward * 0.5f + new Vector3(0, 3, 0);
        Destroy(dynamicObstacle, 20);
    }
    private void recoverFlag()
    {
        flag = 0;
    }
}
