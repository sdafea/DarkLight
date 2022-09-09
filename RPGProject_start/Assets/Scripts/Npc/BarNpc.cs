using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNpc : MonoBehaviour
{

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().ShowPanel<QuestPanel>("QuestPanel");
        }
    }
}
