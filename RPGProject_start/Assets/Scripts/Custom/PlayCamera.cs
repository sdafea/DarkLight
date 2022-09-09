using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamera : MonoBehaviour
{
    private GameObject player;
    Vector3 dir;
    bool isRo;

    private void Start()
    {
        //初始化角色信息
        GameDataMgr.GetInstance().Init();
        ItemMgr.GetInstance().Init();
        //加载角色
        ResMgr.GetInstance().Load<GameObject>("Character/" + GameDataMgr.GetInstance().playerInfo.career);
        player = GameObject.FindGameObjectWithTag(Tags.player);
        transform.LookAt(player.transform.position);
        dir = player.transform.position - transform.position;
        //显示主面板
        UIManager.GetInstance().ShowPanel<MainPanel>("MainPanel", E_UI_Layer.Bot);
    }
    

    private void Update()
    {
        //旋转视角
        transform.position = player.transform.position - dir;
        if (Input.GetMouseButtonDown(1))
        {
            isRo = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRo = false;
        }
        if (isRo)
        {
            transform.RotateAround(player.transform.position, Vector3.up, 30 * Input.GetAxis("Mouse X"));
        }
        dir = player.transform.position - transform.position;
    }
}
