using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class EnterPanel : BasePanel
{
   
    void Start()
    {
        //增加按钮点击事件
        UIManager.AddCustomEventListener(GetControl<Button>("NewGame"), EventTriggerType.PointerDown, (obj)=> {
            New();
        });

        UIManager.AddCustomEventListener(GetControl<Button>("LoadGame"), EventTriggerType.PointerDown, (obj) => {
            Load();
        });
    }

    //进入新游戏
    private void New()
    {
        ScenesMgr.GetInstance().LoadSceneAsyn("charactercreation", () =>
        {
            UIManager.GetInstance().ShowPanel<CharacterPanel>("CharacterPanel");
        });
        UIManager.GetInstance().HidePanel("EnterPanel");
    }

    //加载游戏
    private void Load()
    {
        UIManager.GetInstance().HidePanel("EnterPanel");
        if (File.Exists(GameDataMgr.PlayerInfo_Url))
            ScenesMgr.GetInstance().LoadSceneAsyn("play", () => { });
        else
            Debug.Log("没有存档信息");
    }
}
