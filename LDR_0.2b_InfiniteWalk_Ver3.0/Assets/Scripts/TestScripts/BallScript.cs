using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public AudioClip OpenDoorSound;  //指定需要播放的音效
    private AudioSource source;   //必须定义AudioSource才能调用AudioClip
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(OpenDoorSound, 1F);
    }
}
