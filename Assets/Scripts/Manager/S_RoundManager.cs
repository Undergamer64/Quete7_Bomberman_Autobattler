using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_RoundManager : MonoBehaviour
{
    public static S_RoundManager Instance;

    [SerializeField]
    private float m_Timer = 0f;

    [SerializeField]
    private float m_maxTime = 5f;

    [SerializeField]
    private GameObject m_minutes;

    [SerializeField]
    private GameObject m_hours;

    private float m_rotationMinutes;
    private float m_rotationHours;
    private float m_numberOfHour=0;

    private bool m_TimerOn = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (m_TimerOn)
        {
            m_Timer += 1 * Time.deltaTime;
            m_rotationMinutes = m_Timer / m_maxTime;
            m_rotationHours = m_Timer / m_maxTime;
            m_rotationMinutes = -360 * m_rotationMinutes;
            m_rotationHours = (m_numberOfHour - 30) * m_rotationHours;
            m_hours.transform.eulerAngles = new Vector3(0, 0, m_rotationHours);
            m_minutes.transform.eulerAngles = new Vector3(0, 0, m_rotationMinutes);

        }
        if (m_Timer > m_maxTime)
        {
            m_TimerOn = false;
            m_Timer = 0f;
            m_numberOfHour -=30;
            if(m_numberOfHour <= -360) 
            {
                m_numberOfHour = 0;
            }
            S_ShopManager.Instance.OpenShop();
        }
    }

    public void ChangeTimerState(bool TimerState)
    {
        m_TimerOn = TimerState;
        if (!m_TimerOn)
        {
            m_Timer = 0f;
        }
    }
}
