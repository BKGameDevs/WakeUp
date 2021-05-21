using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanController : MonoBehaviour
{
    public CinemachineVirtualCamera MainVirtualCamera;
    public CameraPanTarget[] Targets;

    private float _OriginalSize;
    private Transform _OriginalTarget;
    private int _Current;

    public float TiltOffset;
    private float _OriginalScreenY;
    // Start is called before the first frame update
    void Start()
    {
        _Current = -1;
        _OriginalSize = MainVirtualCamera.m_Lens.OrthographicSize;
        _OriginalTarget = MainVirtualCamera.Follow;
        _OriginalScreenY = MainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
    }

    public void TiltCamera(object direction)
    {
        var dir = (float) direction;

        if (dir > 0)
            MainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY += TiltOffset;
        else if (dir < 0)
            MainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY -= TiltOffset;
        else
            MainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = _OriginalScreenY;

    }

    public void StartPan()
    {
        RecursivePan();
    }

    private void RecursivePan()
    {
        _Current++;
        if (_Current == Targets.Length)
        {
            _Current = -1;
            //MainVirtualCamera.m_Lens.OrthographicSize = _OriginalSize;
            //MainVirtualCamera.Follow = _OriginalTarget;
            CrossFadeController.Instance.RunCrossFade(() =>
            {
                MainVirtualCamera.m_Lens.OrthographicSize = _OriginalSize;
                MainVirtualCamera.Follow = _OriginalTarget;
            });
            return;
        }

        //TODO: Try to create temporary target for camera to follow and move towards pan targets
        var target = Targets[_Current];
        //this.StartTimedAction(() =>
        //{
        //    MainVirtualCamera.m_Lens.OrthographicSize = target.OrthographicSize;
        //    MainVirtualCamera.Follow = target.Target;
        //},
        //RecursivePan,
        //3f);
        CrossFadeController.Instance.RunCrossFade(() =>
        {
            MainVirtualCamera.m_Lens.OrthographicSize = target.OrthographicSize;
            MainVirtualCamera.Follow = target.Target;
            this.StartTimedAction(null, RecursivePan, 3f);
        });
    }
}

[Serializable]
public struct CameraPanTarget
{
    public Transform Target;
    public float OrthographicSize;
}
