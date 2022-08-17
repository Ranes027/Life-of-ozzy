using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using LifeOfOzzy.UI;
using LifeOfOzzy.Components;
using LifeOfOzzy.Utils;

public class InputEnableComponent : MonoBehaviour
{
    private PlayerInput _input;

    private void Start()
    {
        _input = FindObjectOfType <PlayerInput>();
    }

    public void EnableInput(bool isEnabled)
    {
        _input.enabled = isEnabled;
    } 
}
