using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{

    // ----------------------------------------------------------------------------------------------------------------



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
