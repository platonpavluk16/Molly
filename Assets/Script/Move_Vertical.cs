using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Up : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector3.up * 5f * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector3.down * 5f * Time.deltaTime);
        }
    }
}
