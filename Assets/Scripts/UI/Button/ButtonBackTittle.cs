using System;
using System.Collections;
using System.Collections.Generic;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Mygame.UISystem
{
    public class ButtonBackTittle : MonoBehaviour
    {
        private void Awake()
        {
            // 获取按钮组件
            Button button = GetComponent<Button>();
            // 添加点击事件
            button.onClick.AddListener(BackTittle);
        }

        private void BackTittle()
        {
            UIManager.Instance.BackTittle();
        }
    }
}