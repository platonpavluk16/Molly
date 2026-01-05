using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewWorld()
    {

        int seed = Random.Range(int.MinValue, int.MaxValue);
        
        // 2. Зберігаємо сід світу
        WorldSaveManager.SaveWorld(seed);
        PlayerPrefs.SetInt("WORLD_SEED", seed);


        InventorySaveManager.DeleteInventoryFile();

        SceneManager.LoadScene("Main_Map");
    }

    public void LoadWorld()
    {
        if (!WorldSaveManager.WorldExists())
        {
            Debug.Log("No saved world found!");
            return;
        }


        int seed = WorldSaveManager.LoadWorldSeed();
        PlayerPrefs.SetInt("WORLD_SEED", seed);

        SceneManager.LoadScene("Main_Map");
        

    }
}