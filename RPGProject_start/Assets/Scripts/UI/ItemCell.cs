using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : BasePanel
{
    //当前拖动着的格子
    private ItemCell nowSelItem;
    //当前鼠标进入的格子
    private ItemCell nowInItem;
    //是否拖动中
    private bool isOpenDrag = false;

    public int cellType = 0;
    protected override void Awake()
    {
        base.Awake();

        //监听鼠标移入 和鼠标移除的事件 来进行处理

        EventTrigger trigger = transform.gameObject.AddComponent<EventTrigger>();
        //申明一个 鼠标进入的事件类对象
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(EnterItemCell);

        //申明一个 鼠标移除的事件类对象
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(ExitItemCell);

        trigger.triggers.Add(enter);
        trigger.triggers.Add(exit);

        EventTrigger.Entry beginDrag = new EventTrigger.Entry();
        beginDrag.eventID = EventTriggerType.BeginDrag;
        beginDrag.callback.AddListener(BeginDragItemCell);

        trigger.triggers.Add(beginDrag);


        EventTrigger.Entry drag = new EventTrigger.Entry();
        drag.eventID = EventTriggerType.Drag;
        drag.callback.AddListener(DragItemCell);

        trigger.triggers.Add(drag);

        EventTrigger.Entry endDrag = new EventTrigger.Entry();
        endDrag.eventID = EventTriggerType.EndDrag;
        endDrag.callback.AddListener(EndDragItemCell);

        trigger.triggers.Add(endDrag);

    }

    public void InitInfo(ItemInfo info)
    {
        //加载物品
        GameObject item=ResMgr.GetInstance().Load<GameObject>("Icon/"+info.id.ToString());
        Transform parent = this.transform ;
        item.transform.SetParent(parent);
        item.transform.SetAsFirstSibling();
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        //设置数量
        Transform te = transform.GetChild(1);
        te. gameObject .GetComponent <Text>().text= info.num.ToString();
        OpenDragEvent();
        List<int> listStr = new List<int>();
        listStr.Add(1);
        listStr.Add(2);
        listStr.Add(3);
        foreach (int a in listStr )
        {
            Debug .Log (a);
        }
        listStr.RemoveAt(1);
        Debug.Log(listStr[1]);
    }

    /// <summary>
    /// 开启检测鼠标拖动相关的事件
    /// </summary>
    private void OpenDragEvent()
    {

        if (isOpenDrag)
            return;

        isOpenDrag = true;
        EventTrigger trigger = transform.GetComponent<EventTrigger>();

    }
    private void BeginDragItemCell(BaseEventData data)
    {
        Debug.Log("开始拖动");
        EventCenter.GetInstance().EventTrigger<ItemCell>("ItemCellBeginDrag", this);
    }

    private void DragItemCell(BaseEventData data)
    {
        Debug.Log("拖动中");
        EventCenter.GetInstance().EventTrigger<BaseEventData>("ItemCellDrag", data);
    }

    private void EndDragItemCell(BaseEventData data)
    {
        Debug.Log("结束拖动");
        EventCenter.GetInstance().EventTrigger<ItemCell>("ItemCellEndDrag", this);
    }

    private void EnterItemCell(BaseEventData data)
    {
        Debug.Log("1");
        EventCenter.GetInstance().EventTrigger<ItemCell>("ItemCellEnter", this);
    }

    private void ExitItemCell(BaseEventData data)
    {
        EventCenter.GetInstance().EventTrigger<ItemCell>("ItemCellExit", this);
    }
}
