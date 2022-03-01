using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balabala : MonoBehaviour
{
    [Tooltip("距离")]
    public float distance = 20f;
    [Tooltip("宽度")]
    public float frustumWidth = 40.3f;
    [Tooltip("限制最大的FOV值")]
    public float maxFOV = 65;

    private Camera mainCamera;
    private float cameraAspect;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        CameraAdjust();

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        CameraAdjust();
#endif

    }
    [ContextMenu("执行")]
    private void CameraAdjust()
    {
        if (mainCamera == null) return;
        //分辨率发生改变时才需要重新刷新
        if (cameraAspect.Equals(mainCamera.aspect)) return;

        cameraAspect = mainCamera.aspect;
        //计算Fov
        float _fov = 2 * Mathf.Atan(frustumWidth / cameraAspect * 0.5f / distance) * Mathf.Rad2Deg;
        //防止Fov过大，设置最大值
        if (maxFOV > 0 && _fov > maxFOV)
        {
            _fov = maxFOV;
        }

        mainCamera.fieldOfView = _fov;
    }
}

    
