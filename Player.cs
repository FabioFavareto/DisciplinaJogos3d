using System.Collections;
using System.Collections.Generic;
using SlimUI.ModernMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private Player player;
    private Animator animator;
    public int inventarioMetal = 0;
    public HealthBar healthBar;
    public bool isAlive { get; private set; } = true;
    [SerializeField] private scrDeathMenu menuMorteManager; // Agora arraste o objeto no Inspector
    public TextMeshProUGUI Inventario;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(20);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Recovery(20);
        }
    }

    void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            isAlive = false;
            Morrer();
            return;
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Recovery(int recoveryHealth)
    {
        if (currentHealth > 0 && currentHealth < maxHealth)
        {
            currentHealth += recoveryHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void ReceberDano(int dano)
    {
        currentHealth -= dano;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            isAlive = false;
            Morrer();
        }
    }

    private void Morrer()
    {
        animator.SetBool("isDying", true);
        StartCoroutine(MostrarTelaDeMorteDepoisDaAnimacao());
    }

    private IEnumerator MostrarTelaDeMorteDepoisDaAnimacao()
    {
        // Aguarda até que a animação "Morte" comece
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Morte"))
        {
            yield return null;
        }

        // Aguarda até que a animação "Morte" termine
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Morte") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) // 1.0f indica que a animação terminou
        {
            yield return null;
        }

        // Chama o método para mostrar a tela de morte
        menuMorteManager.MostrarTelaDeMorte();
    }

    public void AdicionarMetal(int quantidade)
    {
        inventarioMetal += quantidade;
        Inventario.text = "Metais coletados: " + inventarioMetal;
    }
}
