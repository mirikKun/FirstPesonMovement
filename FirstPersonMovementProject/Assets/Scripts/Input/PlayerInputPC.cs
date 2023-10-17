using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPC : MonoBehaviour, IInput
{
    public float XInput => GetXInput();
    public float YInput => GetYInput();

    public float XMouseInput => GetXMouseInput();
    public float YMouseInput => GetYMouseInput();

    public bool Jump => GetJumpInput();
    public bool Slide => GetSlideInput();
    public bool Sprint => GetSprintInput();

    private float GetXInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    private float GetYInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    private float GetXMouseInput()
    {
        return Input.GetAxisRaw("Mouse X");
    }

    private float GetYMouseInput()
    {
        return Input.GetAxisRaw("Mouse Y");
    }

    private bool GetJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    private bool GetSlideInput()
    {
        return Input.GetKeyDown(KeyCode.LeftControl);
    }

    private bool GetSprintInput()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
}