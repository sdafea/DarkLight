using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagPanel : BasePanel 
{
    List<ItemCell> list = new List<ItemCell> ();
  
    void Start()
    {
        transform.localPosition = new Vector3(246,0,0);
        Transform grid = transform.GetChild(1);
        foreach (Transform child in grid )
        {
            list.Add(child.GetComponent<ItemCell >());
        }
        InitItem();
    }

    /// <summary>
    /// 加载物品
    /// </summary>
    public void InitItem()
    {
        //获取道具列表信息
        List<ItemInfo> tempInfo = GameDataMgr.GetInstance().playerInfo.items;
        for(int i=0; i<tempInfo .Count;i++)
        {
            if (i > tempInfo.Count - 1)
                return;

            //初始化数据
            list [i].InitInfo(tempInfo[i]);
        }
    }
}
