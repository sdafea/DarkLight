using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    
    void Start()
    {
        //增加按钮点击事件
        UIManager.AddCustomEventListener(GetControl<Button>("Bag"), EventTriggerType.PointerDown, (enter) => {
            BtnBag();
        });
        UIManager.AddCustomEventListener(GetControl<Button>("Equip"), EventTriggerType.PointerDown, (enter) => {
            BtnEquip();
        });
    }

    
    void Update()
    {
        
    }

    private void BtnBag()
    {
        if (UIManager.GetInstance().panelDic.ContainsKey("BagPanel"))
            UIManager.GetInstance().HidePanel("BagPanel");
        else
            UIManager.GetInstance().ShowPanel<BagPanel>("BagPanel");
    }

    private void BtnEquip()
    {
        if (UIManager.GetInstance().panelDic.ContainsKey("EquipPanel"))
        {
            UIManager.GetInstance().HidePanel("BagPanel");
            UIManager.GetInstance().HidePanel("EquipPanel");
        }
        else
        {
            UIManager.GetInstance().ShowPanel<EquipPanel>("EquipPanel");
            UIManager.GetInstance().ShowPanel<BagPanel>("BagPanel");
        }
    }
}
