using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class srcNextLevel : MonoBehaviour
{
    private bool playerNaArea = false;

    public Animator transitionAnim;

    public float transitionTime = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador entrou na área
        {
            playerNaArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador saiu da área
        {
            playerNaArea = false;
        }
    }

    private void Update()
    {
        if (playerNaArea && Input.GetKeyDown(KeyCode.E)) // Apenas permite E quando dentro da área
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            return;
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
