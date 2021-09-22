using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererBitmap : MonoBehaviour
{
    public int[,] Bitmap_Predict;
    private int BITMAPSIZE = 5;
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
        Bitmap_Predict = new int[5, 5];
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                Bitmap_Predict[i, j] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Mathf.Pow(BITMAPSIZE,2); i++)
        {
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
        PrePlanMap();
        
        GetBitmap();
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
        string temp = "";
        for (int i = 0; i < BITMAPSIZE; i++)
        {
            for (int j = 0; j < BITMAPSIZE; j++)
            {
                temp += Bitmap_Predict[i, j].ToString() + "  ";
            }
            temp += " / ";
        }
        Debug.Log(temp);
    }
    private void PlanMap(int i, int j, int value)
    {
        if(i - 1 >= 0 && Bitmap_Predict[i - 1, j] == 0)
        {
            Bitmap_Predict[i - 1, j] = value - 1;
        }
        if(j - 1 >= 0 && Bitmap_Predict[i, j - 1] == 0)
        {
            Bitmap_Predict[i, j - 1] = value - 1;
        }
        if(i + 1 < BITMAPSIZE && Bitmap_Predict[i + 1, j] == 0)
        {
            Bitmap_Predict[i + 1, j] = value - 1;
        }
        if(j + 1 < BITMAPSIZE && Bitmap_Predict[i, j + 1] == 0)
        {
            Bitmap_Predict[i, j + 1] = value - 1;
        }
    }
    private static Hashtable hueColourValues = new Hashtable{
         { new Color( 1, 1, 1, 1 ), HueColorNames.White},
         { new Color( 1, 0, 0, 1 ), HueColorNames.Red},
         { new Color( 0, 0, 1, 1 ), HueColorNames.Blue},
         { new Color( 0.5f, 0.5f, 0.5f, 1 ), HueColorNames.Gray},
         { new Color( 0, 1, 0, 1 ), HueColorNames.Green},
         { new Color( 0, 0, 0, 1 ), HueColorNames.Black},
    };
    public static HueColorNames HueColourValue(Color color)
    {
        return (HueColorNames)hueColourValues[color];
    }
}

