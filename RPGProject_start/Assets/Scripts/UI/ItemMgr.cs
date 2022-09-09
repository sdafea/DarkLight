using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMgr : BaseManager<ItemMgr>
{
    //当前拖动着的物品
    private Transform nowSelGrid;
    //当前鼠标进入的格子
    private Transform nowInGrid;
    //正在移动的格子
    private Transform moveItem;

    //是否拖动中
    private bool isDraging = false;
    public void Init()
    {
        EventCenter.GetInstance().AddEventListener<ItemCell>("ItemCellBeginDrag", BeginDragItemCell);
        EventCenter.GetInstance().AddEventListener<BaseEventData>("ItemCellDrag", DragItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>("ItemCellEndDrag", EndDragItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>("ItemCellEnter", EnterItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>("ItemCellExit", ExitItemCell);
    }

    private void BeginDragItemCell(ItemCell itemCell)
    {
        if (itemCell.transform.childCount < 2)
            return;

        isDraging = true;
        //记录当前选中的格子
        nowSelGrid = itemCell.transform;
        moveItem = nowSelGrid.GetChild(0);
        moveItem.GetComponent<Image>().raycastTarget = false;
    }

    private void DragItemCell(BaseEventData eventData)
    {
        if (isDraging ==false )
            return;

        //把鼠标位置 转换到 UI相关的位置 让 图片跟随鼠标移动
        Vector2 localPos;

        //用于坐标转换的api
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIManager.GetInstance().canvas.GetComponent<RectTransform>(), //希望得到坐标结果对象的 父对象
            (eventData as PointerEventData).position, //相当于 鼠标位置
            (eventData as PointerEventData).pressEventCamera, //相当于 UI摄像机
            out localPos);
        moveItem.SetParent(UIManager.GetInstance().canvas);
        moveItem .localScale = Vector3.one;
        moveItem.transform.localPosition = localPos;
    }

    private void EndDragItemCell(ItemCell itemCell)
    {
        if (isDraging == false )
            return;

        isDraging = false;

        //切换物品
        if (nowInGrid == null)
            ChangeParent(moveItem, nowSelGrid);
        else if(nowInGrid != null&& nowInGrid.childCount <2)
        {
            string te = nowInGrid.GetChild(0).GetComponent<Text>().text;
            nowInGrid.GetChild(0).GetComponent<Text>().text = nowSelGrid.GetChild(0).GetComponent<Text>().text;
            nowSelGrid.GetChild(0).GetComponent<Text>().text = te;
            ChangeParent(moveItem, nowInGrid);
        }
        else
        {
            ChangeParent(moveItem, nowInGrid);
            ChangeParent(nowInGrid.GetChild(1), nowSelGrid);
            string te = nowInGrid.GetChild(1).GetComponent<Text>().text;
            nowInGrid.GetChild(1).GetComponent<Text>().text = nowSelGrid.GetChild(1).GetComponent<Text>().text;
            nowSelGrid.GetChild(1).GetComponent<Text>().text = te;
        }

        //结束拖动时 置空 信息
        nowSelGrid = null;
        nowInGrid = null;
        moveItem.GetComponent<Image>().raycastTarget = true ;
    }

    private void EnterItemCell(ItemCell itemCell)
    {
        Debug.Log("in");
        if (isDraging)
        {
            //拖动中 进入格子  记录进入的信息
            nowInGrid = itemCell.transform;
            return;
        }
    }

    private void ExitItemCell(ItemCell itemCell)
    {
        Debug.Log("out");
        if (isDraging)
        {
            //拖动中 离开格子 清空记录的信息
            nowInGrid = null;
            return;
        }

    }

    public void ChangeParent(Transform item,Transform parent)
    {
        item.SetParent(parent);
        item.transform.SetAsFirstSibling();
        item.localPosition = Vector3.zero;
        item.localScale = Vector3.one;
    }
}
