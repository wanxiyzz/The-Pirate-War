using System.Collections;
using System.Collections.Generic;
using MyGame.HandheldableSystem;
using UnityEngine;
namespace MyGame.HandheldableSystem
{
    public class Spyglass : Handheldable
    {
        protected override void Aim()
        {
            base.Aim();
            CameraManager.Instance.CameraOffsetWithMouse();
        }
    }

}