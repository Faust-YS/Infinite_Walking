using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Fungus;

public class FindObject : MonoBehaviour
{
    public AudioClip CheckSound, ClinkSound;
    private AudioSource Task_source,Flash_source;
    public Camera cameraGameObject;
    private static int taskNum = 0, coinNum = 0;
    public SteamVR_Input_Sources HandType;
    public SteamVR_Action_Vibration HapticAction;
    public GameObject firstWall, nextWall, spaces, trigger;
    public Flowchart flowchart;

    // Start is called before the first frame update
    void Start()
    {
        Task_source = GetComponent<AudioSource>();
        Flash_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TaskPoint")
        {
            Task_source.PlayOneShot(CheckSound, 1F);
            TriggerHapticPulse(0.1f, 100, 1);
            taskNum += 1;
            Debug.Log("TaskPoint: " + taskNum);
            Destroy(other.gameObject);
            if (FinishTaskPoint())
            {
                Debug.Log("Finish!");
                flowchart.ExecuteBlock("Finish");
            }
        }
        if(other.gameObject.tag == "Door")
        {
            if (other.gameObject.transform.localRotation.eulerAngles != new Vector3(0, 0, 0))
            {
                other.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else other.gameObject.transform.rotation = new Quaternion(0, -0.7f, -0.7f, 0);
            initialize(other.gameObject.name);
            TriggerHapticPulse(0.1f, 100, 1);
        }
        if (other.gameObject.tag == "coin")
        {
            coinNum += 1;
            Flash_source.PlayOneShot(ClinkSound, 1F);
            //Destroy(other.gameObject);
            TriggerHapticPulse(0.1f, 100, 1);
        }
    }

    public void TriggerHapticPulse(float duration, float frequency, float amplitude)
    {
        HapticAction.Execute(0, duration, frequency, amplitude, HandType);
    }
    private void initialize(string name)
    {
        print(name);
        spaces.SetActive(true);
        firstWall.SetActive(true);
        nextWall.SetActive(false);
        trigger.SetActive(true);
        if (name == "Plane_001") spaces.transform.position = new Vector3(41.25f, 0.01f, 17.5f);
        else if (name == "Plane_002") spaces.transform.position = new Vector3(41.25f, 0.01f, 9.5f);
        else if (name == "Plane_003") spaces.transform.position = new Vector3(41.25f, 0.01f, 1.5f);
        else if (name == "Plane_004") spaces.transform.position = new Vector3(41.25f, 0.01f, -6.5f);
        else spaces.SetActive(false);
    }
    public bool FinishTaskPoint()
    {
        if (taskNum > 9) return true;
        else return false;
    }
    public int GetCoins(int body = 0)
    {
        if(body == 1)
        {
            coinNum++;
        }
        return coinNum;
    }
    public int GetTaskNum()
    {
        return taskNum;
    }
}
