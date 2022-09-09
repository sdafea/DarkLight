using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = transform.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry drag = new EventTrigger.Entry();
        drag.eventID = EventTriggerType.Drag;
        drag.callback.AddListener(DragItemCell);

        trigger.triggers.Add(drag);

    }

    private void DragItemCell(BaseEventData data)
    {
        Debug.Log("拖动");
        //把鼠标位置 转换到 UI相关的位置 让 图片跟随鼠标移动
        Vector2 localPos;

        //用于坐标转换的api
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform .parent .parent .GetComponent<RectTransform>(), //希望得到坐标结果对象的 父对象
            (data as PointerEventData).position, //相当于 鼠标位置
            (data as PointerEventData).pressEventCamera, //相当于 UI摄像机
            out localPos);
    }
}
