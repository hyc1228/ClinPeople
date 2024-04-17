using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ComboInput : MonoBehaviour
{
    public InputActionAsset inputActions;

    public InputAction m_lightAttack;
    public InputAction m_heavyAttack;

    private void OnEnable() => inputActions?.Enable();
    private void OnDisable() => inputActions?.Disable();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_lightAttack = inputActions["LightAttack"];
        m_heavyAttack = inputActions["HeavyAttack"];
    }

    public bool GetLightAttackDown() => m_lightAttack.WasPressedThisFrame();
    public bool GetHeavyAttackDown() => m_heavyAttack.WasPressedThisFrame();

}
