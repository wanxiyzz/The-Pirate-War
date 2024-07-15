using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fashhoot : MonoBehaviour
{
    public Action<Transform> HookSomething;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FloatBarrel"))
        {
            HookSomething?.Invoke(other.gameObject.transform);
        }
    }
}
