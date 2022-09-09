using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{
    bool isDone = false;
    Vector3 targetPoint;
    private Animation animation;

    void Start()
    {
        animation = this.GetComponent<Animation>();
    }


    void Update()
    {
        if (UIManager.GetInstance().panelDic.Count == 1|| UIManager.GetInstance().panelDic.Count == 0)
        {
            //鼠标点击触发移动
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isCollider = Physics.Raycast(ray, out hitInfo);
                if (isCollider && hitInfo.collider.tag == Tags.ground)
                {
                    Instantiate((GameObject)Resources.Load("Click/Efx_Click_Green"), hitInfo.point, Quaternion.identity);
                    targetPoint = hitInfo.point;
                    isDone = true;
                }
            }

            if (isDone == true && Vector3.Distance(transform.position, targetPoint) > 0.6f)
            {
                animation.Play("Run");
                transform.LookAt(targetPoint);
                transform.Translate(Vector3.forward * Time.deltaTime * 6);
            }

            if (Vector3.Distance(transform.position, targetPoint) <= 0.6f)
            {
                isDone = false;
                animation.Play("Idle");
            }
        }
        

    }
    
}
