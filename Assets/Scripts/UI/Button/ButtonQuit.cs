using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem.Buttons
{
    public class ButtonQuit : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(QuitGame);
        }
        void QuitGame()
        {
            Application.Quit();
        }
    }
}