using System;
using UnityEngine;

public class KeyboardInputService : IInputService
{
    public Vector2 MoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
