using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Main_Player : MonoBehaviour
{

    public int HP;

    void Update()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5);
        }
        if(Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        if(Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }
        if(Keyboard.current.dKey.isPressed)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 5);
        }
    }
}
