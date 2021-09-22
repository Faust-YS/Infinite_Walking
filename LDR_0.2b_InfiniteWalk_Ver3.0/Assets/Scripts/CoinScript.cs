using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Transform preScattered;
    private ParticleSystem Scattered;
    private Vector3 newPos;
    private bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        Scattered = Instantiate(preScattered).GetComponent<ParticleSystem>();
        newPos = this.transform.localPosition + new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //transform.position = new Vector3(transform.position.x, 1.2f + Mathf.PingPong(Time.time*0.05f, 0.2f), transform.position.z);
        if (locked == true)
        {
            ScatteredDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller"/* || other.gameObject.tag == "Player"*/)
        {
            locked = true;
        }
    }
    private void ScatteredDestroy()
    {
        this.transform.Rotate(0, 360 * Time.deltaTime * 5f, 0);
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, newPos, Time.deltaTime * 5f);
        Scattered.transform.position = transform.position;
        Scattered.Simulate(0.0f, true, true);
        Scattered.Play();
        Destroy(this.gameObject,0.5f);
    }
}
