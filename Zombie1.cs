using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    public int zombieHealth = 100;
    public int presentHealth;
    public int giveDamage = 10;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingPointRadius = 2;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie mood/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRadius;

    [Header("Zombie Animations")]
    public Animator anim;

    private srcDropItems dropScript;

    private void Awake()
    {
        presentHealth = zombieHealth;
        zombieAgent = GetComponent<NavMeshAgent>();
        zombieAgent.speed = zombieSpeed;
        dropScript = GetComponent<srcDropItems>();
    }

    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInVisionRadius && !playerInAttackingRadius) Guard();
        if (playerInVisionRadius && !playerInAttackingRadius) PursuePlayer();
        if (playerInVisionRadius && playerInAttackingRadius) AttackPlayer();
    }

    private void Guard()
    {
        zombieAgent.speed = zombieSpeed;
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
        }

        // Movimenta o zumbi usando o NavMeshAgent para o ponto de patrulha atual
        zombieAgent.SetDestination(walkPoints[currentZombiePosition].transform.position);

        // Rotaciona o zumbi para olhar para o ponto atual
        Vector3 direction = (walkPoints[currentZombiePosition].transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * zombieSpeed);
    }

    private void PursuePlayer()
    {
        zombieAgent.speed = zombieSpeed * 3f;
        if (zombieAgent.SetDestination(playerBody.position))
        {
            // animations
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        } else
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
    }

    private void AttackPlayer()
    {
        // Parar o agente para realizar o ataque
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);

        if (!previouslyAttack)
        {
            // Verificar se o player estÃ¡ dentro do raio de ataque
            float distanceToPlayer = Vector3.Distance(transform.position, playerBody.position);

            if ((distanceToPlayer - 0.5) <= attackingRadius)
            {
                // Obter o componente Player no objeto alvo e aplicar o dano
                Player player = playerBody.GetComponent<Player>();
                if (player != null)
                {
                    anim.SetBool("Walking", false);
                    anim.SetBool("Running", false);
                    anim.SetBool("Attacking", true);
                    anim.SetBool("Died", false);
                    player.ReceberDano(giveDamage);
                }
            }
            
            // Marcar como tendo atacado e aguardar o tempo entre ataques
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }


    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    public void TakeDamage(int takeDamage)
    {
        presentHealth -= takeDamage;

        if (presentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
        anim.SetBool("Died", true);
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInAttackingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
        dropScript.DropItem();
    }
}