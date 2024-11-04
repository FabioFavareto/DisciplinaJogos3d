using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMovimento : MonoBehaviour
{
    public float veloc = 1f;
    public float gravidade = -9.81f;
    public float pulo = 15f;
    public Transform orientation; // Referência para a orientação da câmera
    private Animator animator;

    private CharacterController controle;
    private bool noChao;
    private Vector3 velocPerson;
    private bool canMove = true;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isStandingUp = stateInfo.IsName("Standing Up");
        bool isMorte = stateInfo.IsName("Morte");

        if (isStandingUp || isMorte)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            // Verificar se o personagem está no chão
            noChao = controle.isGrounded;

            // Parar o cálculo da gravidade ao tocar no chão e adicionar um pequeno valor negativo para manter o personagem no chão
            if (noChao && velocPerson.y < 0)
            {
                velocPerson.y = -2f; // Mantém uma força leve para garantir que o personagem toque o chão
                animator.SetBool("isJumping", false); // Define isJumping como false quando no chão
            }

            // Movimento no eixo X e Z
            float deslocX = Input.GetAxis("Horizontal");
            float deslocZ = Input.GetAxis("Vertical");

            // Criar vetor de movimento relativo à orientação da câmera
            Vector3 direcaoMovimento = orientation.forward * deslocZ + orientation.right * deslocX;
            direcaoMovimento.y = 0f; // Para evitar movimento vertical indesejado

            // Normalizar o vetor para manter a velocidade constante ao mover na diagonal
            direcaoMovimento.Normalize();

            // Verificar se o personagem está andando
            bool isWalking = deslocX != 0 || deslocZ != 0;

            // Atualizar o parâmetro "isWalking" no Animator
            animator.SetBool("isWalking", isWalking);

            // Movimento do personagem
            controle.Move(direcaoMovimento * veloc * Time.deltaTime);

            // Implementação do pulo
            if (Input.GetKeyDown(KeyCode.Space) && noChao)
            {
                animator.SetBool("isJumping", true); // Iniciar a animação de 
                velocPerson.y = Mathf.Sqrt(pulo * -4f * gravidade); // Calcula a força do pulo
            }

            // Aplicar gravidade
            velocPerson.y += gravidade * Time.deltaTime;

            // Aplicar a movimentação vertical
            controle.Move(velocPerson * Time.deltaTime);
        }
    }
}
