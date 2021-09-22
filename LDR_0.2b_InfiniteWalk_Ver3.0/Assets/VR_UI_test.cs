using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class VR_UI_test : MonoBehaviour
{
    public GameObject d1, d2, d3 = null;
    public Text Score, Key = null;
    private FindObject findObject;
    public GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        d1 = GameObject.Find("MenuDialog");
        d2 = GameObject.Find("SayDialog");
        d3 = GameObject.Find("Result");
        Score = GameObject.Find("Score_Num").GetComponent<Text>();
        Key = GameObject.Find("Key_Num").GetComponent<Text>();
        findObject = new FindObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (d1 == null)
        {
            d1 = GameObject.Find("MenuDialog");
        }
        if (d2 == null)
        {
            d2 = GameObject.Find("SayDialog");
        }
        if (d1 != null)
        {
            d1.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            d1.transform.parent = cam.transform;
            d1.transform.localPosition = new Vector3(0, -0.5f, 1);
            d1.transform.localRotation = Quaternion.Euler(0, 0, 0);
            d1.transform.localScale = new Vector3(0.00075f, 0.00075f, 0.01f);
        }
        if (d2 != null)
        {
            d2.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            d2.transform.parent = cam.transform;
            d2.transform.localPosition = new Vector3(0, 0, 1);
            d2.transform.localRotation = Quaternion.Euler(0, 0, 0);
            d2.transform.localScale = new Vector3(0.00075f, 0.00075f, 0.01f);
        }
        if (d3 != null)
        {
            d3.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            d3.transform.parent = cam.transform;
            d3.transform.localPosition = new Vector3(0, 0.5f, 1);
            d3.transform.localRotation = Quaternion.Euler(0, 0, 0);
            d3.transform.localScale = new Vector3(0.003f, 0.003f, 0.03f);
        }

        Result();
    }

    private void Result()
    {
        Score.text = findObject.GetCoins().ToString();
        Key.text = findObject.GetTaskNum().ToString();
        // Score.color = Color.red;
        // Key.color = Color.red;
    }
}
