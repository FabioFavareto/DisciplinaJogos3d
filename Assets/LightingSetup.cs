using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingSetup : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DynamicGI.UpdateEnvironment();  // Atualiza a iluminação global dinâmica
    }
}
