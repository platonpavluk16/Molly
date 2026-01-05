using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Tp_script : MonoBehaviour
{
    [SerializeField] private string[] scenes;
    [SerializeField] private float cooldown = 15f;

    private string lastScene = "";
    private float lastUseTime = -Mathf.Infinity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;


        if (Time.time - lastUseTime < cooldown) return;

        lastUseTime = Time.time;

        string nextScene = GetRandomScene();
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private string GetRandomScene()
    {
        if (scenes.Length == 0) return null;

        int worldSeed = PlayerPrefs.GetInt("WORLD_SEED", 0);
        System.Random rnd = new System.Random(worldSeed + DateTime.Now.Millisecond);

        string scene;
        int attempts = 0;

        do
        {
            int index = rnd.Next(0, scenes.Length);
            scene = scenes[index];
            attempts++;
            if (attempts > 10) break; 
        } while (scene == lastScene);

        lastScene = scene;
        return scene;
    }
}
