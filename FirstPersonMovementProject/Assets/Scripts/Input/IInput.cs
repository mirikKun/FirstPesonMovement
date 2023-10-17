using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public float XInput { get; }
    public float YInput { get; }
    public bool Jump { get; }
    public bool Slide { get; }
    public bool Sprint { get; }
    float XMouseInput { get; }
    float YMouseInput { get; }
}
