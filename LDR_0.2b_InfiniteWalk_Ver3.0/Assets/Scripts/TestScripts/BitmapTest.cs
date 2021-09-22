using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitmapTest : MonoBehaviour
{
    public int[,] Bitmap;
    private int BITMAPSIZE = 5;
    private Transform bucket;
    private Transform[] tmpCoin;
    private GameObject tempObstacle;
    int coin = 0;
    public enum HueColorNames
    {
        White,
        Red,
        Blue,
        Gray,
    }
    // Start is called before the first frame update
    void Start()
    {
        Bitmap = new int[5, 5];
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                Bitmap[i, j] = 0;
            }
        }
        bucket = Resources.Load<Transform>("rv_bucket");
        tempObstacle = null;
        tmpCoin = new Transform[5];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject plane = GameObject.Find("Plane (" + i + ")");
            if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.White)
            {
                Bitmap[(int)i / 5, (int)i % 5] = 2; // None -> 可能會走也可以走
            }
            else if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Red)
            {
                Bitmap[(int)i / 5, (int)i % 5] = -1; // Physical Boundary -> 可能會走但不能走
            }
            else if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Blue)
            {
                Bitmap[(int)i / 5, (int)i % 5] = 1; // Virtual Obstacle -> 不會走但可以走
            }
            else if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Gray)
            {
                Bitmap[(int)i / 5, (int)i % 5] = 0; // Both -> 不會走也不能走
            }
        }
        string temp = "";
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                temp += Bitmap[i, j].ToString() + "  ";
            }
            temp += " / ";
        }
        Debug.Log(temp);
        if(tempObstacle == null)
        {
            PopUpDyObs();
        }
        coin = GameObject.FindGameObjectsWithTag("coin").Length;
        if(coin < 5)
        {
            PopUpCoins();
        }
    }
    private static Hashtable hueColourValues = new Hashtable{
         { new Color( 1, 1, 1, 1 ), HueColorNames.White},
         { new Color( 1, 0, 0, 1 ), HueColorNames.Red},
         { new Color( 0, 0, 1, 1 ), HueColorNames.Blue},
         { new Color( 0.5f, 0.5f, 0.5f, 1 ), HueColorNames.Gray},
    };
    public static HueColorNames HueColourValue(Color color)
    {
        return (HueColorNames)hueColourValues[color];
    }
    
    public void PopUpDyObs()
    {
        if(Bitmap[1,2] == -1)
        {
            GameObject plane = GameObject.Find("Plane (7)");
            tempObstacle = Instantiate(bucket.gameObject);
            tempObstacle.transform.position = plane.transform.position + new Vector3(0,2,0);
            Destroy(tempObstacle.gameObject, 10);
        }
    } 
    public void PopUpCoins()
    {
        if(Bitmap[3,3] == -1)
        {
            GameObject plane = GameObject.Find("Plane (18)");
            tmpCoin[coin] = Instantiate(Resources.Load<Transform>("coin"));
            tmpCoin[coin].position = new Vector3(Random.Range(-1.0f, 1.0f), 0.3f, Random.Range(-1.0f, 1.0f));
            tmpCoin[coin].GetComponent<Rigidbody>().useGravity = true;
            tmpCoin[coin].GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
