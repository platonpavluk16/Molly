using UnityEngine;
using UnityEngine.InputSystem;

public class Switch_Control : MonoBehaviour
{


    void Start()
    {
        Move_Horizontal move_Horizontal = GetComponent<Move_Horizontal>();
        Move_Up move_Vertical = GetComponent<Move_Up>();

        move_Horizontal.enabled = true;
        move_Vertical.enabled = false;
    }


    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Move_Horizontal move_Horizontal = GetComponent<Move_Horizontal>();
            Move_Up move_Vertical = GetComponent<Move_Up>();

            if (move_Horizontal.enabled)
            {
                move_Horizontal.enabled = false;
                move_Vertical.enabled = true;
            }
            else if (move_Vertical.enabled)
            {
                move_Vertical.enabled = false;
                move_Horizontal.enabled = true;
                
            }
        }
    }
}
