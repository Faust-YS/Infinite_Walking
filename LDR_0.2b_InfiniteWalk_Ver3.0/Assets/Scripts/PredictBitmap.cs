using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictBitmap : MonoBehaviour
{
    public int[,] Bitmap_Danger;
    public int[,] Bitmap_Predict;
    public int[,] Bitmap_Total;
    private int BITMAPSIZE = 5;
    private bool getLineCollider;
    private bool putCoin, putObstacle;
    public GameObject preCoin, preObstacle;
    private static Vector3 CoinPosition, ObstaclePosition;
    public enum HueColorNames
    {
        White,
        Red,
        Blue,
        Gray,
        Green,
        Black
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Bitmap_Danger = new int[5,5];
        Bitmap_Predict = new int[5, 5];
        Bitmap_Total = new int[5, 5];
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                Bitmap_Danger[i, j] = 0;
                Bitmap_Predict[i, j] = 0;
                Bitmap_Total[i, j] = 0;
            }
        }
        getLineCollider = false;
        putCoin = true;
        putObstacle = true;
    }

    // Update is called once per frame
    void Update()
    {
        getLineCollider = CollisionTrigger.GetLineCollider();
        for (int i = 0; i < Mathf.Pow(BITMAPSIZE, 2); i++)
        {
            GameObject plane = GameObject.Find("Plane (" + i + ")");
            
            if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.White)
            {
                Bitmap_Danger[(int)i / 5,(int)i % 5] = -1; // None -> 可能會走也可以走
            }
            else if(HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Red)
            {
                Bitmap_Danger[(int)i / 5,(int)i % 5] = 3; // Physical Boundary -> 可能會走但不能走
            }
            else if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Blue)
            {
                Bitmap_Danger[(int)i / 5,(int)i % 5] = 1; // Virtual Obstacle -> 不會走但可以走
            }
            else if (HueColourValue(plane.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Gray)
            {
                Bitmap_Danger[(int)i / 5,(int)i % 5] = 2; // Both -> 不會走也不能走
            }

            GameObject map = GameObject.Find("map (" + i + ")");
            if (HueColourValue(map.GetComponent<Renderer>().material.GetColor("_Color")) == HueColorNames.Green)
            {
                Bitmap_Predict[(int)i / 5, (int)i % 5] = 10;
            }
            else
            {
                Bitmap_Predict[(int)i / 5, (int)i % 5] = 0;
            }
        }
        if(getLineCollider == true)
        {
            PrePlanMap();
            GetBitmap();
            CombineBitmap();
            SetObject();
        } 
    }
    private void PrePlanMap()
    {
        bool done = true;
        int value = 10;
        while (done == true)
        {
            done = false;
            for (int i = 0; i < Mathf.Pow(BITMAPSIZE, 2); i++)
            {
                if (Bitmap_Predict[(int)i / 5, (int)i % 5] == value)
                {
                    PlanMap((int)i / 5, (int)i % 5, value);
                }
                if (Bitmap_Predict[(int)i / 5, (int)i % 5] == 0)
                {
                    done = true;
                }
            }
            value--;
        }
    }
    private void GetBitmap()
    {
        string temp1 = "";
        string temp2 = "";
        string total = "";
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                temp1 += Bitmap_Danger[i, j].ToString() + "  ";
                temp2 += Bitmap_Predict[i, j].ToString() + "  ";
                total += Bitmap_Total[i, j].ToString() + "  ";
            }
            temp1 += " / ";
            temp2 += " / ";
            total += " / ";
        }
        //Debug.Log(temp1 + "\n" + temp2);
        Debug.Log(total);
    }
    private void PlanMap(int i, int j, int value)
    {
        if (i - 1 >= 0 && Bitmap_Predict[i - 1, j] == 0)
        {
            Bitmap_Predict[i - 1, j] = value - 1;
        }
        if (j - 1 >= 0 && Bitmap_Predict[i, j - 1] == 0)
        {
            Bitmap_Predict[i, j - 1] = value - 1;
        }
        if (i + 1 < BITMAPSIZE && Bitmap_Predict[i + 1, j] == 0)
        {
            Bitmap_Predict[i + 1, j] = value - 1;
        }
        if (j + 1 < BITMAPSIZE && Bitmap_Predict[i, j + 1] == 0)
        {
            Bitmap_Predict[i, j + 1] = value - 1;
        }
    }
    private void CombineBitmap()
    {
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                if (i == 0)
                {
                    Bitmap_Total[i, j] = Bitmap_Danger[i,j] * Bitmap_Predict[i,j] * 2;
                }
                else if(i == 1)
                {
                    Bitmap_Total[i, j] = Bitmap_Danger[i, j] * Bitmap_Predict[i, j] * 3;
                }
                else if(i == 2 || (i > 2 && (j == 0 || j == 4)))
                {
                    Bitmap_Total[i, j] = Bitmap_Danger[i, j] * Bitmap_Predict[i, j] * 4;
                }
                else if(i == 4 && j == 2)
                {
                    Bitmap_Total[i, j] = Bitmap_Danger[i, j] * Bitmap_Predict[i, j] * 0;
                }
                else
                {
                    Bitmap_Total[i, j] = Bitmap_Danger[i, j] * Bitmap_Predict[i, j] * 5;
                }
            }
        }
    }
    private void ExistObject()
    {
        if(GameObject.Find("DynamicCoin(Clone)") == null)
        {
            putCoin = true;
        }
        if(GameObject.Find("DynamicObstacle(Clone)") == null)
        {
            putObstacle = true;
        }
    }
    private void GetCoin()
    {
        putCoin = true;
    }
    private void GetObstacle()
    {
        putObstacle = true;
    }
    private void SetObject()
    {
        CoinPosition = new Vector3(0, 0, 0);
        ObstaclePosition = new Vector3(0, 0, 0);
        int index = 0;
        if (putCoin == true)
        {
            int minPredict = -30;
            for (int i = 0; i < BITMAPSIZE; i++)
            {
                for (int j = 0; j < BITMAPSIZE; j++)
                {
                    if (Bitmap_Total[i,j] < minPredict/* && Bitmap_Total[i,j] < 0*/)
                    {
                        index = i * 5 + j;
                        GameObject map = GameObject.Find("map (" + index + ")");
                        CoinPosition = map.transform.position + new Vector3(0, 1.5f, 0);
                        print(index + " , CoinPosition: " + CoinPosition);
                        minPredict = Bitmap_Total[i, j];
                    }
                }
            }
            GameObject coin = Instantiate(preCoin, CoinPosition, new Quaternion(0, 0, 0, 0));
            putCoin = false;
            Destroy(coin, 30);
            Invoke("GetCoin", 5);
        }
        if(putObstacle == true)
        {
            int maxDanger = 60;
            for (int i = 0; i < BITMAPSIZE; i++)
            {
                for (int j = 0; j < BITMAPSIZE; j++)
                {
                    if (Bitmap_Total[i, j] > maxDanger)
                    {
                        index = i * 5 + j;
                        GameObject plane = GameObject.Find("Plane (" + index + ")");
                        ObstaclePosition = plane.transform.position;
                        //print(index + " , ObstaclePosition: " + ObstaclePosition);
                        maxDanger = Bitmap_Total[i, j];
                    }
                }
            }
            print("ObstaclePosition: " + ObstaclePosition);
            GameObject obstacle = Instantiate(preObstacle, ObstaclePosition, new Quaternion(0, 0, 0, 0));
            putObstacle = false;
            Invoke("GetObstacle", 30);
            Destroy(obstacle, 20);
        }
    }
    public static Vector3 GetObstaclePosition()
    {
        return ObstaclePosition;
    }

    private static Hashtable hueColourValues = new Hashtable{
         { new Color( 1, 1, 1, 1 ), HueColorNames.White},
         { new Color( 1, 0, 0, 1 ), HueColorNames.Red},
         { new Color( 0, 0, 1, 1 ), HueColorNames.Blue},
         { new Color( 0.5f, 0.5f, 0.5f, 1 ), HueColorNames.Gray},
         { new Color( 0, 1, 0, 1 ), HueColorNames.Green},
         { new Color( 0, 0, 0, 1 ), HueColorNames.Black}
    };
    public static HueColorNames HueColourValue(Color color)
    {
        return (HueColorNames)hueColourValues[color];
    }
}
