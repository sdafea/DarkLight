using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : BasePanel 
{
    List<ItemCell> list = new List<ItemCell>();

    void Start()
    {
        transform.localPosition = new Vector3(-362, 0, 0);
        for (int i=1;i<7;i++)
        {
            list.Add(transform .GetChild (i).GetComponent<ItemCell>());
            list[i - 1].cellType = i;
        }
        InitItem();
    }

    /// <summary>
    /// 加载物品
    /// </summary>
    public void InitItem()
    {
        //获取道具列表信息
        List<ItemInfo> tempInfo = GameDataMgr.GetInstance().playerInfo.nowEquip;
        for (int i = 0; i < tempInfo.Count; i++)
        {
            if (i > tempInfo.Count - 1)
                return;

            //初始化数据
            switch (GameDataMgr.GetInstance().GetItemInfo(tempInfo[i].id).equipType)
            {
                case 1:
                    list[1].InitInfo(tempInfo[i]);
                    break;
                case 2:
                    list[2].InitInfo(tempInfo[i]);
                    break;
                case 3:
                    list[3].InitInfo(tempInfo[i]);
                    break;
                case 4:
                    list[4].InitInfo(tempInfo[i]);
                    break;
                case 5:
                    list[5].InitInfo(tempInfo[i]);
                    break;
                case 6:
                    list[6].InitInfo(tempInfo[i]);
                    break;
            }
        }
    }
}
