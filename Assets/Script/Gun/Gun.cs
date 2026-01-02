using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{    

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float offset = 90f;

    void Update()
    {
    Vector3 mousePos = Mouse.current.position.ReadValue();
    Vector3 difference = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset);


    if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
    }
}
