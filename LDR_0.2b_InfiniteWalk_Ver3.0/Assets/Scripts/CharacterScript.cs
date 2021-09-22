using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CharacterScript : MonoBehaviour
{
    private Transform body;
    public Flowchart flowchart;
    private float posX, posZ;
    private string[] hintBox =
    {
        "怎麼沒有日期和今天的值日生在黑板上呢？",
        "第二排座位的桌子上好像有甚麼東西？",
        "自習的時候可以到後面的座位上拿課外書來閱讀喔！",
        "咦？好像有人掉東西在那邊的椅子上耶！",
        "老師剛剛請我去他辦公桌的抽屜上拿東西！",
        "聽說那幅畫價值上千萬耶！",
        "唉，我東西被校長沒收在他座位後面的櫃子了。",
        "校長好像出差了，辦公桌上都沒有來過的痕跡。",
        "檢查看看椅子周圍有沒有忘記帶的東西。",
        "請問你有看到垃圾桶在哪裡嗎？"
    };

    void Start()
    {
        body = GameObject.FindGameObjectWithTag("Player").transform;
        this.transform.LookAt(body);
        this.transform.rotation = Quaternion.Euler(90, 0, 0);
        
        posX = this.transform.position.x;
        posZ = this.transform.position.z;

        Hint();
    }

    void Update()
    {
        
    }
    private void Hint()
    {
        flowchart = this.gameObject.AddComponent<Flowchart>();
        Block block = flowchart.CreateBlock(Vector2.zero);
        block.BlockName = "HintBlock";

        Say say = this.gameObject.AddComponent<Say>();
        say.ParentBlock = block;
        say.ItemId = flowchart.NextItemId();
        say.CommandIndex = block.CommandList.Count;
        say.OnCommandAdded(block);

        if (WhichRoom() == 1)
        {
            say.SetStandardText(hintBox[Random.Range(0, 3)]);
            block.CommandList.Add(say);
        }
        else if (WhichRoom() == 2)
        {
            say.SetStandardText(hintBox[Random.Range(3, 5)]);
            block.CommandList.Add(say);
        }
        else if (WhichRoom() == 3)
        {
            say.SetStandardText(hintBox[Random.Range(5, 8)]);
            block.CommandList.Add(say);
        }
        else if (WhichRoom() == 4)
        {
            say.SetStandardText(hintBox[Random.Range(8, 10)]);
            block.CommandList.Add(say);
        }
        block.StartExecution();
    }
    public int WhichRoom()
    {
        int n = 0;
        if(posX>43 && posX<47)
        {
            if(posZ > 13 && posZ < 20)
            {
                n = 1;
            }
            else if(posZ > 5 && posZ < 12)
            {
                n = 2;
            }
            else if (posZ > 5 && posZ < 12)
            {
                n = 2;
            }
            else if (posZ > -3 && posZ < 4)
            {
                n = 3;
            }
            else if (posZ > -11 && posZ < -4)
            {
                n = 4;
            }
        }
        return n;
    }
}
