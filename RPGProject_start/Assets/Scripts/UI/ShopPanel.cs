using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : BasePanel 
{
    private Transform content;

    public override void ShowMe()
    {
        base.ShowMe();
        content = GameObject .Find("Content").transform ;
        //显示商店面板时 初始化 商店面板中的售卖信息
        //根据 商店数据 来进行初始化
        for (int i = 0; i < GameDataMgr.GetInstance().shopInfos.Count; i++)
        {
            ShopItem item = ResMgr.GetInstance().Load<GameObject>("UI/ShopItem").GetComponent<ShopItem>();
            item.transform.SetParent(content);
            item.transform.localScale = Vector3.one;
            item.InitInfo(GameDataMgr.GetInstance().shopInfos[i]);
        }
    }
}
