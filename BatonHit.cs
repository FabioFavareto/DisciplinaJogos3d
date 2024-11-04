using System.Collections;
using UnityEngine;

public class BatonHitController : MonoBehaviour
{
    public int danoBastao = 10;
    private bool isAttacking = false;
    private Animator animator;
    private Zombie1 HitEnemy;
    private float attackDelay = 0.3f; // Tempo para aplicar o dano no meio do ataque

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("batonHit", true);

            // Cria um raycast a partir da câmera para a posição do mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica se o raycast colidiu com algum objeto
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica se o objeto atingido tem o componente 'Zombie1'
                HitEnemy = hit.collider.GetComponent<Zombie1>();
                if (HitEnemy != null)
                {
                    // Aplica o dano após o atraso definido
                    Invoke("ApplyDamage", attackDelay);
                }
            }

            // Inicia a coroutine para resetar o valor de 'batonHit' após o tempo de animação
            StartCoroutine(ResetBatonHit());
        }
    }

    // Função para aplicar o dano no alvo
    private void ApplyDamage()
    {
        if (HitEnemy != null)
        {
            HitEnemy.TakeDamage(danoBastao);
            HitEnemy = null; // Limpa a referência após aplicar o dano
        }
    }

    private IEnumerator ResetBatonHit()
    {
        yield return new WaitForSeconds(0.5f); // Ajuste para o tempo da animação
        isAttacking = false;
        animator.SetBool("batonHit", false);
    }
}