﻿using UnityEngine;

public class Revolution : MonoBehaviour 
{
    //围绕旋转的目标
    public Transform RevolutionTarget;
    //围绕旋转的物体
    public Transform RevolutionSelf;
    //x轴轨道偏离量
    public float XDeviation = 0;
    //y轴轨道偏离量
    public float YDeviation = 0;
    //z轴轨道偏离量
    public float ZDeviation = 0;
    //x轴旋转半径
    public float XRotateSpeed = 2;
    //y轴旋转半径
    public float YRotateSpeed = 2;
    //z轴旋转半径
    public float ZRotateSpeed = 2;
    //轨道速度
    public float RevolutionSpeed = 50;
    //显示轨道
    public bool ShowGizmos = false;
    
    private float _radius;
    private float _Angle;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _radius = Vector3.Distance(RevolutionTarget.position, RevolutionSelf.position);
        _Angle = 0;
    }
	
	private void Update() 
	{
        Going();
    }
    
    private void Going()
    {
        if (RevolutionTarget == null || RevolutionSelf == null)
            return;

        _Angle += RevolutionSpeed * Time.deltaTime;
        if (_Angle > 360) _Angle -= 360;

        float x = RevolutionTarget.position.x + XDeviation + Mathf.Cos(_Angle * (Mathf.PI / 180)) * _radius * XRotateSpeed;
        float y = RevolutionTarget.position.y + YDeviation + Mathf.Sin(_Angle * (Mathf.PI / 180)) * _radius * YRotateSpeed;
        float z = RevolutionTarget.position.z + ZDeviation + Mathf.Sin(_Angle * (Mathf.PI / 180)) * _radius * ZRotateSpeed;

        RevolutionSelf.position = new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        if (!ShowGizmos || RevolutionTarget == null || RevolutionSelf == null)
            return;
        
        //绘制目标线
        Gizmos.color = Color.red;
        Gizmos.DrawLine(RevolutionTarget.position, RevolutionSelf.position);

        //绘制轨道
        Gizmos.color = Color.blue;
        float sub = 0.1f;
        float i = 0;
        //轨道起点
        float x = RevolutionTarget.position.x + XDeviation + _radius * Mathf.Cos(i) * XRotateSpeed;
        float y = RevolutionTarget.position.y + YDeviation + _radius * Mathf.Sin(i) * YRotateSpeed;
        float z = RevolutionTarget.position.z + ZDeviation + _radius * Mathf.Sin(i) * ZRotateSpeed;
        Vector3 sPoint = new Vector3(x, y, z);
        Vector3 ePoint = Vector3.zero;
        for (i += sub; i <= Mathf.PI * 2; i += sub)
        {
            x = RevolutionTarget.position.x + XDeviation + _radius * Mathf.Cos(i) * XRotateSpeed;
            y = RevolutionTarget.position.y + YDeviation + _radius * Mathf.Sin(i) * YRotateSpeed;
            z = RevolutionTarget.position.z + ZDeviation + _radius * Mathf.Sin(i) * ZRotateSpeed;
            ePoint = new Vector3(x, y, z);
            Gizmos.DrawLine(sPoint, ePoint);
            sPoint = ePoint;
        }
        //轨道终点
        x = RevolutionTarget.position.x + XDeviation + _radius * Mathf.Cos(Mathf.PI * 2) * XRotateSpeed;
        y = RevolutionTarget.position.y + YDeviation + _radius * Mathf.Sin(Mathf.PI * 2) * YRotateSpeed;
        z = RevolutionTarget.position.z + ZDeviation + _radius * Mathf.Sin(Mathf.PI * 2) * ZRotateSpeed;
        ePoint = new Vector3(x, y, z);
        Gizmos.DrawLine(sPoint, ePoint);
    }
}
