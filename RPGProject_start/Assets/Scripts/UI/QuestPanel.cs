using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestPanel : BasePanel
{
    public int KillNum;
    int num;
    bool accept;
    void Start()
    {
        num = GameDataMgr.GetInstance().playerInfo.task1Num;
        accept = GameDataMgr.GetInstance().playerInfo.task1Accept;

        //增加按钮点击事件
        UIManager.AddCustomEventListener(GetControl<Button>("Accept"), EventTriggerType.PointerDown, (enter) => {
            BtnAccept();
        });
        UIManager.AddCustomEventListener(GetControl<Button>("Cancel"), EventTriggerType.PointerDown, (enter) => {
            BtnCancel();
        });
        UIManager.AddCustomEventListener(GetControl<Button>("Ok"), EventTriggerType.PointerDown, (enter) =>
        {
            BtnCancel();
            BtnOk();
        });
        transform.Find("Ok").gameObject.SetActive(false);

        if(accept == true)
            AcceptPanel();

    }

    private void BtnAccept()
    {
        EventCenter.GetInstance().EventTrigger("Task1");
        AcceptPanel();
    }

    private void BtnCancel()
    {
        UIManager.GetInstance().HidePanel("QuestPanel");
    }

    private void BtnOk()
    {
        if (num >= 10)
        {
            EventCenter.GetInstance().EventTrigger("Task1","a");
            EventCenter.GetInstance().EventTrigger("Money",1000);
        }

    }
    private void AcceptPanel()
    {
        transform.GetComponentInChildren<Text>().text = "任务：\n已杀死" + num + "/10只小野狼\n\n奖励：\n1000金币";
        transform.Find("Accept").gameObject.SetActive(false);
        transform.Find("Cancel").gameObject.SetActive(false);
        transform.Find("Ok").gameObject.SetActive(true);
    }
}
