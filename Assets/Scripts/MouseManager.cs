using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool _cursorVisible = false;

    void Start()
    {
        SetCursor(_cursorVisible);
    }

    public void SetCursor(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
