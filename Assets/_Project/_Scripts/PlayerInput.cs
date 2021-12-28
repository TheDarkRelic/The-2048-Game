using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public bool Left;
    [HideInInspector] public bool Right;
    [HideInInspector] public bool Up;
    [HideInInspector] public bool Down;

    private void Update()
    {
        Left = Input.GetKeyDown(KeyCode.A);
        Right = Input.GetKeyDown(KeyCode.D);
        Up = Input.GetKeyDown(KeyCode.W);
        Down = Input.GetKeyDown(KeyCode.S);
    }
    
}
