using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionNpc : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().ShowPanel<ShopPanel>("ShopPanel");
        }
    }
}
