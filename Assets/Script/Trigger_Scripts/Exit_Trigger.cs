using UnityEngine;
using UnityEditor;

public class Exit_Trigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
    #if UNITY_EDITOR
    EditorApplication.isPlaying = false;
    #else
    Application.Quit(); 
    #endif
        }
    }
}
