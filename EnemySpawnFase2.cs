using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnFase2 : MonoBehaviour
{
    public GameObject enemyPrefab; // O prefab do inimigo a ser instanciado
    public Transform spawnPoint;   // O ponto inicial do spawn (voc� pode usar a posi��o do pr�prio objeto SpawnArea)
    public float spawnInterval = 5f; // Intervalo entre os spawns
    private bool playerInside = false; // Para verificar se o player est� dentro do trigger
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se � o jogador
        {
            playerInside = true;
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Para de instanciar quando o jogador sair da �rea
        {
            playerInside = false;
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (playerInside)
        {
            Vector3 randomPosition = GetRandomPositionWithinBounds();
            Instantiate(enemyPrefab, randomPosition, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomPositionWithinBounds()
    {
        // Obtenha as dimens�es do Box Collider
        Vector3 center = boxCollider.bounds.center;
        Vector3 size = boxCollider.bounds.size;

        // Gere uma posi��o aleat�ria dentro do Box Collider
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        // A posi��o Y ser� mantida a mesma do objeto de spawn (ou pode ser alterada conforme necess�rio)
        return new Vector3(randomX, spawnPoint.position.y, randomZ);
    }
}
