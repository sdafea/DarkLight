using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel1 : BasePanel
{
    private  void Delete()
    {
        UIManager.GetInstance().HidePanel("TipPanel1");
    }
    public void InitInfo(string info)
    {
        GetControl<Text>("txtInfo").text = info;
        Invoke("Delete", 0.6f);
    }

}
