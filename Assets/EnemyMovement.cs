using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // O jogador ou alvo que o inimigo vai seguir
    public float speed = 3f; // Velocidade do inimigo
    public float stoppingDistance = 1.5f; // Distância mínima que o inimigo deve manter do jogador
    public int health = 100; // Vida do inimigo
    public int damageToPlayer = 10; // Dano que o inimigo causa ao jogador
    public float attackCooldown = 2f; // Tempo entre os ataques do inimigo
    public float gravity = -9.81f; // Gravidade aplicada ao inimigo

    private float attackTimer;
    private Animator animator;
    private Boolean isDead = false;
    public event System.Action OnDeath;
    private Vector3 velocity; // Para armazenar a velocidade vertical
    private Rigidbody rb; // Referência para o Rigidbody

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = 0f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // Obter o Rigidbody
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (isDead) return;

            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                transform.position += direction * speed * Time.deltaTime;
            }
            else if (attackTimer <= 0f)
            {
                AtacarJogador();
                attackTimer = attackCooldown;
            }
        }


        // Aplicar gravidade
        if (rb != null)
        {
            if (rb.isKinematic)
            {
                rb.isKinematic = false; // Certifique-se que o Rigidbody não é kinematic
            }

            // Verificar se o inimigo está no chão
            if (Physics.Raycast(transform.position, Vector3.down, 1f)) // Ajuste a distância conforme necessário
            {
                velocity.y = -2f; // Mantém uma leve força para garantir que o inimigo esteja no chão
            }
            else
            {
                velocity.y += gravity * Time.deltaTime; // Aplicar gravidade
            }

            // Atualizar a movimentação vertical
            rb.velocity = new Vector3(rb.velocity.x, velocity.y, rb.velocity.z);
        }
    }

    // Método para aplicar dano ao inimigo
    public void ReceberDano(int dano)
    {
        health -= dano;

        if (health <= 0)
        {
            Morrer();
        }
    }

    // Simula o inimigo atacando o jogador
    private void AtacarJogador()
    {
        Player playerHealth = target.GetComponent<Player>();

        if (playerHealth != null)
        {
            playerHealth.ReceberDano(damageToPlayer);
        }
    }

    // Método chamado quando o inimigo morre
    private void Morrer()
    {
        isDead = true;
        animator.SetBool("isDeath", true);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            //rb.isKinematic = false; // Permite que a gravidade afete o inimigo
            // rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Opcional: força para simular queda
        }

        if (OnDeath != null)
        {
            OnDeath();
        }

        Destroy(gameObject, 5f);
    }
}