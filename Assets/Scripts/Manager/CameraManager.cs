using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera vCam;
    private Coroutine fieldVisionCoroutine;
    private Coroutine rotateCameraCoroutine;
    private CinemachineTransposer transposer;
    private CinemachineFramingTransposer framingTransposer;
    void Start()
    {
        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
        framingTransposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    /// <summary>
    /// 调整视距
    /// </summary>
    public void FieldVision(float zoom)
    {
        if (fieldVisionCoroutine != null)
        {
            StopCoroutine(fieldVisionCoroutine);
            fieldVisionCoroutine = null;
        }
        fieldVisionCoroutine = StartCoroutine(ChangerFieldVission(zoom, 25));
    }
    IEnumerator ChangerFieldVission(float zoom, float frames)
    {
        float startZoom = vCam.m_Lens.OrthographicSize;
        float t = 0;
        while (t < frames)
        {
            t++;
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(startZoom, zoom, t / frames);
            yield return Setting.waitForFixedUpdate;
        }
        vCam.m_Lens.OrthographicSize = zoom;
    }
    /// <summary>
    /// 调成视角旋转
    /// </summary>
    public void RotateCamera(Quaternion rotation)
    {
        if (rotateCameraCoroutine != null)
        {
            StopCoroutine(rotateCameraCoroutine);
            rotateCameraCoroutine = null;
        }
        rotateCameraCoroutine = StartCoroutine(IERotateCamera(rotation * Quaternion.Euler(0, 0, -90)));
    }
    IEnumerator IERotateCamera(Quaternion angle)
    {
        Quaternion startAngle = mainCamera.transform.rotation;
        float t = 0;
        while (t < 25)
        {
            t++;
            mainCamera.transform.rotation = Quaternion.Lerp(startAngle, angle, t / 25);
            yield return Setting.waitForFixedUpdate;
        }
    }
    /// <summary>
    ///还原视角的旋转
    /// </summary>
    public void ResetCamera()
    {
        if (rotateCameraCoroutine != null)
        {
            StopCoroutine(rotateCameraCoroutine);
            rotateCameraCoroutine = null;
        }
        rotateCameraCoroutine = StartCoroutine(IERotateCamera(new Quaternion(0, 0, 0, 1)));
    }
    /// <summary>
    /// 调整视角的偏转
    /// </summary>
    public void ChangeCameraOffset(float offsetX = 0.5f, float offsetY = 0.5f)
    {
        framingTransposer.m_ScreenX = offsetX;
        framingTransposer.m_ScreenY = offsetY;
    }
}
