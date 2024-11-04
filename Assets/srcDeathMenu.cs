using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrDeathMenu : MonoBehaviour
{
    public GameObject deathScreenCanvas;
    // Start is called before the first frame update
    void Start()
    {
        deathScreenCanvas.SetActive(false);
    }

    public void ReiniciarCena()
    {
        deathScreenCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SairDoJogo()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void MostrarTelaDeMorte()
    {
        Cursor.visible = true; // Torna o cursor visível
        Cursor.lockState = CursorLockMode.None; // Desbloqueia o cursor
        deathScreenCanvas.SetActive(true);
    }
}