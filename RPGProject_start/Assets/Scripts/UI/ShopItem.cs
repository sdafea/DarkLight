using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : BasePanel
{
    private ShopCellInfo info;

    void Start()
    {
        GetControl<Button>("btnBuy").onClick.AddListener(ButItem);
    }

    /// <summary>
    /// 买物品的函数处理
    /// </summary>
    private void ButItem()
    {
        if (info.itemInfo.num * info.price > GameDataMgr.GetInstance().playerInfo.money)
        {
            //提示
            TipMgr.GetInstance().ShowOneBtnTip("货币不足");
        }
        else 
        {
            EventCenter.GetInstance().EventTrigger("Money", -info.itemInfo.num * info.price);
            //添加物品给玩家
            EventCenter.GetInstance().EventTrigger("AddItem", info .itemInfo);
        }
    }

    /// <summary>
    /// 初始化 商店物品 复合控件的显示信息
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ShopCellInfo info)
    {
        this.info = info;

        Item item = GameDataMgr.GetInstance().GetItemInfo(info.itemInfo.id);
        Transform icon = ResMgr.GetInstance().Load<GameObject>("Icon/" + info.itemInfo.id.ToString()).transform;
        icon.SetParent(transform .GetChild (0));
        icon.SetAsFirstSibling();
        icon.localPosition = Vector3.zero;
        icon.localScale = Vector3.one;
        GetControl<Text>("Text").text = info.itemInfo.num.ToString();
        GetControl<Text>("txtName").text = "名称："+item.name;
        GetControl<Text>("txtPrice").text = "价格："+info.price.ToString();
        GetControl<Text>("txtTips").text = "效果：" + info.tips;
    }
}
