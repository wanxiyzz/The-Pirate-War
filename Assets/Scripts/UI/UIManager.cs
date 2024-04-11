using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text hemlText;
    void Start()
    {

    }

    void Update()
    {

    }
    public void EnterHelm(float helmRotate)
    {
        hemlText.gameObject.SetActive(true);
        UpdateHeml(helmRotate);
    }
    public void UpdateHeml(float helmRotate)
    {
        hemlText.text = "舵角：" + helmRotate.ToString("f2");
    }
}
