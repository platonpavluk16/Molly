using UnityEngine;

public class TriggerHideObject2D : MonoBehaviour
{
    [SerializeField] private GameObject objectToHide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectToHide.SetActive(false);
    }
}
