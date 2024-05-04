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

    //震动
    private CinemachineBasicMultiChannelPerlin cinemaPerlin;
    private Coroutine cameraShakeCoroutine;
    [SerializeField] Quaternion targetRotation;
    [SerializeField] float rotationSpeed = 5;
    protected override void Awake()
    {
        base.Awake();
        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
        framingTransposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        cinemaPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemaPerlin.m_AmplitudeGain = 0;
    }
    public void SetFollow(Transform player)
    {
        vCam.Follow = player;
    }
    private void Update()
    {
        if (transform.rotation != targetRotation)
        {
            Quaternion rotationToTarget = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = rotationToTarget;
        }
    }
    public void RotateCamera(Quaternion rotation)
    {
        targetRotation = rotation;
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
        fieldVisionCoroutine = StartCoroutine(ChangerFieldVission(zoom, 50f));
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
    IEnumerator IERotateCamera(Quaternion angle)
    {
        Quaternion startAngle = transform.rotation;
        float t = 0;
        while (t < 50)
        {
            t++;
            transform.rotation = Quaternion.Lerp(startAngle, angle, t / 50f);
            yield return Setting.waitForFixedUpdate;
        }
    }
    /// <summary>
    ///还原视角的旋转
    /// </summary>
    public void ResetCamera()
    {
        targetRotation = new Quaternion(0, 0, 0, 1);
    }
    /// <summary>
    /// 调整视角的偏转
    /// </summary>
    public void ChangeCameraOffset(float offsetX = 0.5f, float offsetY = 0.5f)
    {
        framingTransposer.m_ScreenX = offsetX;
        framingTransposer.m_ScreenY = offsetY;
    }
    /// <summary>
    /// 控制相机的震动
    /// </summary>
    /// <param name="time"></param>
    public void CameraShake(float time, float amplitudeGain)
    {
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
            cameraShakeCoroutine = null;
        }
        cameraShakeCoroutine = StartCoroutine(CameraShke(time, amplitudeGain));
    }
    IEnumerator CameraShke(float time, float amplitudeGain)
    {
        cinemaPerlin.m_AmplitudeGain = amplitudeGain;
        yield return new WaitForSeconds(time);
        cinemaPerlin.m_AmplitudeGain = 0f;
    }
    public void CameraOffsetWithMouse()
    {
        if (Input.GetMouseButton(1))
        {
            ChangeCameraOffset(-GetNormalizedMousePosition().x * 2 + 0.5f, GetNormalizedMousePosition().y * 2 + 0.5f);
        }
        else
        {
            ChangeCameraOffset();
        }
    }
    private Vector3 GetNormalizedMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        // 归一化鼠标坐标
        mousePosition.x = mousePosition.x / Screen.width - 0.5f;
        mousePosition.y = mousePosition.y / Screen.height - 0.5f;
        return mousePosition;
    }
}
