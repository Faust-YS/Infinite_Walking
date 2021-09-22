using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleTest : MonoBehaviour
{
    private Vector3 start, end, difference, temp;
    private float seconds = 1, timer = 0, percent;


    // Start is called before the first frame update
    void Start()
    {

        transform.position = PredictBitmap.GetObstaclePosition();
        start = transform.position;
        end = start + new Vector3(0, 4, 0);
        difference = end - start;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= seconds)
        {
            timer += Time.deltaTime;
            percent = timer / seconds;
            transform.position = start + difference * percent;
        }
    }
}
