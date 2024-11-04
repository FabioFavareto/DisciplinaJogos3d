using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srcItem : MonoBehaviour
{
    public int valorDoMetal = 1; // Valor do metal coletado (1 unidade)
    public float distanciaParaColetar = 2f; // Distancia para a coleta

    private GameObject player; // Referencia ao jogador

    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // Encontra o objeto do jogador pela tag
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanciaParaColetar)
        {
            ColetarMetal();
        }
    }

    private void ColetarMetal()
    {
        player.GetComponent<Player>().AdicionarMetal(valorDoMetal); // Adiciona metal ao inventario do jogador
        Destroy(gameObject); // Destroi o objeto metal do chao
    }
}