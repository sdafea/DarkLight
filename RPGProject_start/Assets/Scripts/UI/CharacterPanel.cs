using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterPanel : BasePanel
{
    private InputField inputField;
    void Start()
    {
        //初始化角色信息
        GameDataMgr.GetInstance().Init();
        //加载角色
        ResMgr.GetInstance().LoadAsync<GameObject>("Character/Magician",(obj)=> { });

        //增加按钮点击事件
        UIManager.AddCustomEventListener(GetControl<Button>("Next"), EventTriggerType.PointerDown, (enter) => {
            BtnDown();
        });

        UIManager.AddCustomEventListener(GetControl<Button>("Prev"), EventTriggerType.PointerDown, (enter) => {
            BtnDown();
        });

        UIManager.AddCustomEventListener(GetControl<Button>("OK"), EventTriggerType.PointerDown, (enter) => {
            BtnOk();
        });
    }

    /// <summary>
    /// 切换角色
    /// </summary>
    private void BtnDown()
    {
        if (GameObject.Find("Magician(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Magician(Clone)"));
            ResMgr.GetInstance().LoadAsync<GameObject>("Character/Swordman", (obj) => { });
        }
        else
        {
            GameObject.Destroy(GameObject.Find("Swordman(Clone)"));
            ResMgr.GetInstance().LoadAsync<GameObject>("Character/Magician", (obj) => { });
        }
    }

    private void BtnOk()
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        EventCenter.GetInstance().EventTrigger("NameChange", inputField.text);
        if (GameObject.Find("Magician(Clone)") == null)
        {
            EventCenter.GetInstance().EventTrigger("CareerChange", "Swordman");
        }
        else
        {
            EventCenter.GetInstance().EventTrigger("CareerChange", "Magician");
        }
        ScenesMgr.GetInstance().LoadSceneAsyn("charactercreation", () =>
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("play", () =>{ });
            UIManager.GetInstance().HidePanel("CharacterPanel");
        });
    }
}
