using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotator : MonoBehaviour
{
    private const float EPSILON = 0.0001f;
    private const float SMOOTH_DAMP_SEC_FUDGE = 0.1f;
    public DragRotatorInfo m_info;
    private float m_pitchDeg;
    private float m_rollDeg;
    private float m_pitchVel;
    private float m_rollVel;
    private Vector3 m_prevPos;
    private Vector3 m_originalAngles;

    private void Awake()
    {
        Reset();
        this.m_prevPos = this.transform.position;
        this.m_originalAngles = this.transform.localRotation.eulerAngles;

    }

    private void Update()
    {
        Vector3 position = this.transform.position;
        Vector3 vector3 = position - this.m_prevPos;
        if ((double)vector3.sqrMagnitude > 9.99999974737875E-05)
        {
            this.m_pitchDeg += vector3.y * this.m_info.m_PitchInfo.m_ForceMultiplier;
            this.m_pitchDeg = Mathf.Clamp(this.m_pitchDeg, this.m_info.m_PitchInfo.m_MinDegrees, this.m_info.m_PitchInfo.m_MaxDegrees);
            this.m_rollDeg -= vector3.x * this.m_info.m_RollInfo.m_ForceMultiplier;
            this.m_rollDeg = Mathf.Clamp(this.m_rollDeg, this.m_info.m_RollInfo.m_MinDegrees, this.m_info.m_RollInfo.m_MaxDegrees);
        }
        this.m_pitchDeg = Mathf.SmoothDamp(this.m_pitchDeg, 0.0f, ref this.m_pitchVel, this.m_info.m_PitchInfo.m_RestSeconds * 0.1f);
        this.m_rollDeg = Mathf.SmoothDamp(this.m_rollDeg, 0.0f, ref this.m_rollVel, this.m_info.m_RollInfo.m_RestSeconds * 0.1f);
        this.transform.localRotation = Quaternion.Euler(this.m_originalAngles.x + this.m_pitchDeg, this.m_originalAngles.y + this.m_rollDeg, this.m_originalAngles.z);
        this.m_prevPos = position;
    }

    public DragRotatorInfo GetInfo()
    {
        return this.m_info;
    }

    public void SetInfo(DragRotatorInfo info)
    {
        this.m_info = info;
    }

    public void Reset()
    {
        this.m_prevPos = this.transform.position;
        this.transform.localRotation = Quaternion.Euler(this.m_originalAngles);
        this.m_rollDeg = 0.0f;
        this.m_rollVel = 0.0f;
        this.m_pitchDeg = 0.0f;
        this.m_pitchVel = 0.0f;
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseUp()
    {
        //logic for hand or collider on board/target
    }

}