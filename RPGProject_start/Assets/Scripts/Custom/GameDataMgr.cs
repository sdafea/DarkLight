using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class GameDataMgr : BaseManager<GameDataMgr>
{
    //玩家信息存储路径
    public static string PlayerInfo_Url = "E:/Unity/Demo/PlayerInfo.json";

    // 玩家数据
    public Player playerInfo;

    //持有物品数据
    private Dictionary<int, Item> itemInfos = new Dictionary<int, Item>();
    //商店数据
    public List<ShopCellInfo> shopInfos;


    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init()
    {
        
        //初始化玩家信息
        if (File.Exists(PlayerInfo_Url))
        {
            //读出指定路径的文件的字节数组
            byte[] bytes = File.ReadAllBytes(PlayerInfo_Url);
            //把字节数组转成字符串
            string json = Encoding.UTF8.GetString(bytes);
            //再把字符串转成玩家的数据结构
            playerInfo = JsonUtility.FromJson<Player>(json);
        }
        else
        {
            playerInfo = new Player();
            SavePlayerInfo();
        }

        //背包
        //加载Resources文件夹下的json文件 获取它的内容
        string info = ResMgr.GetInstance().Load<TextAsset>("Json/ItemInfo").text;
        //根据json文件的内容 解析成对应的数据结构 并存储起来
        Items items = JsonUtility.FromJson<Items>(info);
        for (int i = 0; i<items.info.Count; i++)
        {
            itemInfos.Add(items.info[i].id, items.info[i]);
        }

        //商店
        //加载Resources文件夹下的json文件 获取它的内容
        string shopInfo = ResMgr.GetInstance().Load<TextAsset>("Json/ShopInfo").text;
        //根据json文件的内容 解析成对应的数据结构 并存储起来
        Shops shopsInfo = JsonUtility.FromJson<Shops>(shopInfo);
        //记录下 加载解析出来的商店信息
        shopInfos = shopsInfo.info;

        //监听名字改变的事件
        EventCenter.GetInstance().AddEventListener<string>("NameChange", ChangeName);
        //监听职业改变的事件
        EventCenter.GetInstance().AddEventListener<string>("CareerChange", ChangeCareer);
        //监听任务改变的事件
        EventCenter.GetInstance().AddEventListener<string>("Task1Num", ChangeTask1num );
        //监听任务状态的事件
        EventCenter.GetInstance().AddEventListener<string>("Task1", ChangeTask1);
        //监听金钱改变的事件
        EventCenter.GetInstance().AddEventListener<int>("Money", Changemoney);
        //监听物品改变的事件
        EventCenter.GetInstance().AddEventListener<ItemInfo>("AddItem", AddItem);
    }


    /// <summary>
    /// 保存玩家信息
    /// </summary>
    private  void SavePlayerInfo()
    {
        //并且存储它
        string jsonStr = JsonUtility.ToJson(playerInfo);
        File.WriteAllText("E:/Unity/Demo/PlayerInfo.json", jsonStr);
    }

    /// <summary>
    /// 改变名字
    /// </summary>
    /// <param name="name"></param>
    private  void ChangeName(string name)
    {
        playerInfo.ChangeName(name);
        SavePlayerInfo();
    }

    /// <summary>
    /// 改变职业
    /// </summary>
    /// <param name="name"></param>
    private void ChangeCareer(string career)
    {
        playerInfo.ChangeCareer(career);
        SavePlayerInfo();
    }

    /// <summary>
    /// 改变任务数值
    /// </summary>
    /// <param name="name"></param>
    private void ChangeTask1num(string num)
    {
        playerInfo.Changetask1Num();
        SavePlayerInfo();
    }
    /// <summary>
    /// 改变任务状态
    /// </summary>
    /// <param name="name"></param>
    private void ChangeTask1(string num)
    {
        playerInfo.Changetask1();
        SavePlayerInfo();
    }
    /// <summary>
    /// 改变金钱
    /// </summary>
    /// <param name="name"></param>
    private void Changemoney(int num)
    {
        playerInfo.ChangeMoney(num);
        SavePlayerInfo();
    }
    /// <summary>
    /// 改变物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private void AddItem(ItemInfo info)
    {
        playerInfo.AddItem(info);
        SavePlayerInfo();
    }

    public Item GetItemInfo(int id)
    {
        if (itemInfos.ContainsKey(id))
            return itemInfos[id];
        return null;
    }
}

/// <summary>
/// 玩家基础信息
/// </summary>
public class Player
{
    public string name;
    public int lev;
    public int money;
    public int gem;
    public int pro;
    public string career;
    public int task1Num;
    public bool task1Accept=false;
    public int equipType;

    public List<ItemInfo> items;
    public List<ItemInfo> nowEquip;

    private bool isOn = false;

    public Player()
    {
        name = " ";
        lev = 1;
        money = 0;
        gem = 0;
        pro = 0;
        career = "Magician";
        task1Num=0;

        items = new List<ItemInfo>() { new ItemInfo() { id = 3, num = 99 }, new ItemInfo() { id = 1, num = 10 } };
        nowEquip = new List<ItemInfo>() { };
    }

    public void ChangeName(string name)
    {
        this.name = name;
    }

    public void ChangeCareer(string career)
    {
        this.career = career;
    }
    public void Changetask1Num()
    {
        this.task1Num=task1Num++;
    }
    public void Changetask1()
    {
        if(this.task1Accept == true)
            this.task1Accept = false;
        else
            this.task1Accept = true;
    }
    public void ChangeMoney(int money)
    {
        this.money +=money;
    }

    public  void AddItem(ItemInfo info)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == info.id)
            {
                items[i].num += info.num;
                isOn = true;
            }
        }
        if(isOn ==false)
            items.Add(info);
    }

    //public void DeleteItem(ItemInfo info)
    //{
    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        if (items[i].id == info.id)
    //        {
    //            items[i].num += info.num;
    //            isOn = true;
    //        }
    //    }
    //    if (isOn == false)
    //        items.Add(info);
    //}
}

/// <summary>
/// 玩家拥有的道具基础信息
/// </summary>
[System.Serializable]
public class ItemInfo
{
    public int id;
    public int num;
}

/// <summary>
/// 临时结构体 用来表示 道具表信息的数据结构
/// </summary>
public class Items
{
    public List<Item> info;
}

/// <summary>
/// 道具的基础信息 数据结构
/// </summary>
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string icon;
    public int type;
    public int price;
    public string tips;
    public int equipType;
}

/// <summary>
/// 作为json读取的中间数据结构 用来装载json内容
/// </summary>
public class Shops
{
    public List<ShopCellInfo> info;
}

/// <summary>
/// 商店售卖物品信息的数据
/// </summary>
[System.Serializable]
public class ShopCellInfo
{
    public int id;
    public ItemInfo itemInfo;
    public int priceType;
    public int price;
    public string name;
    public string tips;
}
