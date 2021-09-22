using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredDestroy : MonoBehaviour
{
    private bool locked = false;
    public ParticleSystem Scattered;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            locked = true;
        }
        if(locked == true)
        {
            Destroying();
        }
    }
    private void Destroying()
    {
        transform.Rotate(0, 360 * Time.deltaTime * 5f, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 1, 0), Time.deltaTime * 5f);
        Scattered.transform.position = transform.position;
        Scattered.Simulate(0.0f, true, true);
        Scattered.Play();
        Destroy(this.gameObject, 0.5f);
    }
}
