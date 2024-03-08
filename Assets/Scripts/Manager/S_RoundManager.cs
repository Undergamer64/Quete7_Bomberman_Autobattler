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
        }
        if (m_Timer > m_maxTime)
        {
            m_TimerOn = false;
            m_Timer = 0f;
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
