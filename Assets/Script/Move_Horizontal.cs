using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Horizontal : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }
    }
}
