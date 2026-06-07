using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    // ----------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        Debug.Log($"GSM Awake: {gameObject.name}, Instance={Instance}");

        if (Instance != null && Instance != this)
        {
            Debug.Log($"Duplicate GSM destroyed: {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log($"GSM persisted: {gameObject.name}");
    }



    // ----------------------------------------------------------------------------------------------------------------


    public void StartGame()
    {
        Debug.Log("StartGame() clicked");
        SceneManager.LoadScene("PrototypeMap");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame() clicked");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu() clicked");
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadThisScene(string scene)
    {
        Debug.Log($"Loading scene {scene}");
        SceneManager.LoadScene(scene);
    }
}
