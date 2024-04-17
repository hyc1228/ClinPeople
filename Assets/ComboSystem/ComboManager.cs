using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComboManager : MonoBehaviour
{
    public WeaponConfig currentWeaponConfig;
    public float releaseTime;

    private Animator m_animator;
    private ComboInput m_comboInput;

    private float m_releaseTimer;
    private bool m_isOnNeceTime;
    private ComboConfig m_currentComboConfig;

    private int m_lightAttackIdx = 0;
    private int m_heavyAttackIdx = 0;

    public const float m_animationFadeTime = 0.1f;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if(m_releaseTimer < Time.time)
            StopCombo();
        HandleCombo();
    }

    private void Init()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_comboInput = GetComponent<ComboInput>();
    }

    private void HandleCombo()
    {
        if(m_isOnNeceTime)
            return;
        if(m_comboInput.GetLightAttackDown())
            NormalAttack(true);
        else if(m_comboInput.GetHeavyAttackDown())
            NormalAttack(false);
    }

    IEnumerator PlayCombo(ComboConfig comboConfig)
    {
        m_isOnNeceTime = true;
        m_releaseTimer = Time.time + releaseTime;
        m_currentComboConfig = comboConfig;
        
        m_animator.CrossFade(comboConfig.m_animatorStateName, m_animationFadeTime);
        float timeOrigin = 0f;
        while (true)
        {
            if (timeOrigin >= comboConfig.m_releaseTime)
                break;
            timeOrigin += Time.deltaTime;
            yield return null;
        }

        m_isOnNeceTime = false;
        yield break;
    }

    public void NormalAttack(bool isLight)
    {
        List<ComboConfig> configs =
            isLight ? currentWeaponConfig.m_lightComboConfigs : currentWeaponConfig.m_heavyComboConfigs;
        int comboIdx = isLight ? m_lightAttackIdx : m_heavyAttackIdx;

        StartCoroutine(PlayCombo(configs[comboIdx]));

        if (comboIdx >= configs.Count - 1)
            comboIdx = 0;
        else
            comboIdx++;

        if (isLight)
            m_lightAttackIdx = comboIdx;
        else
        {
            m_heavyAttackIdx = comboIdx;
        }
        
    }

    private void StopCombo()
    {
        m_lightAttackIdx = 0;
        m_heavyAttackIdx = 0;
    }
    
}
