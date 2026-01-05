using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 10f;
    public float nightDuration = 10f;
    public float transitionSpeed = 0.5f;
    public Color nightColor = new Color(0, 0, 0, 0.9f);

    public GameObject gun;
    public Spawner enemySpawner; 
    public Camera mainCamera;

    private SpriteRenderer sr;
    private float timer;
    private bool isNight;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (mainCamera == null) mainCamera = Camera.main;
        
        sr.color = new Color(nightColor.r, nightColor.g, nightColor.b, 0f);
        
        if (gun != null) gun.SetActive(false);
        if (enemySpawner != null) enemySpawner.stopSpawning = true;
        
        ResizeAndPosition();
    }

    void LateUpdate()
    {
        transform.position = mainCamera.transform.position + new Vector3(0, 0, 1);

        timer += Time.deltaTime;
        float currentMaxDuration = isNight ? nightDuration : dayDuration;

        if (timer >= currentMaxDuration)
        {
            timer = 0f;
            isNight = !isNight;

            if (gun != null) gun.SetActive(isNight);
            if (enemySpawner != null) enemySpawner.stopSpawning = !isNight;

            if (!isNight)
            {
                KillAllEnemies();
            }
        }

        float targetAlpha = isNight ? nightColor.a : 0f;
        float currentAlpha = sr.color.a;
        float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime * transitionSpeed);
        
        sr.color = new Color(nightColor.r, nightColor.g, nightColor.b, newAlpha);
    }

    void KillAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void ResizeAndPosition()
    {
        if (mainCamera == null) return;
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        transform.localScale = new Vector3(width, height, 1f);
    }
}