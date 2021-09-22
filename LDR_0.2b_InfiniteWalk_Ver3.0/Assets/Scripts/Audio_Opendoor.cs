using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Opendoor : MonoBehaviour
{

    public AudioClip KickBallSound;  //指定需要播放的音效
    private AudioSource source;   //必须定义AudioSource才能调用AudioClip


    void Start()
    {
        source = GetComponent<AudioSource>();  //将this Object 上面的Component赋值给定义的AudioSource
    }

    // Update is called once per frame
    void Update()
    {
        //if (PlayerController.Process == "MissionAception")   //当条件触发
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            source.PlayOneShot(KickBallSound, 1F);   //播放声音
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            print("Trigger the Ball");
            source.PlayOneShot(KickBallSound, 1F);   //播放声音
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            print("Collision the Ball");
            source.PlayOneShot(KickBallSound, 1F);   //播放声音
        }
    }
}
