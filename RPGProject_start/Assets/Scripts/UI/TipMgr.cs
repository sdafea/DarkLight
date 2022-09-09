using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipMgr : BaseManager<TipMgr>
{
    /// <summary>
    /// 显示一键提示面板
    /// </summary>
    /// <param name="info"></param>
    public void ShowOneBtnTip(string info)
    {
        UIManager.GetInstance().ShowPanel<TipPanel1>("TipPanel1", E_UI_Layer.System, (panel) =>
        {
            panel.InitInfo(info);
        });
    }
}
