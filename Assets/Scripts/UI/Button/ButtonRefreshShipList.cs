using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRefreshShipList : MonoBehaviour
{
    private void Awake()
    {
        // 获取按钮组件
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => EventHandler.CallCountShipsAndPlayers());
    }
}
