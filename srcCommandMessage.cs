using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class srcCommandMessage : MonoBehaviour
{
    public TextMeshProUGUI texto;
    [Range(0.1f, 10.0f)] public float distancia = 3;
    private GameObject Jogador;
    private float tempoExibicao = 6.0f;
    private float timer = 0.0f;
    private float tempoDeitado = 13.0f;
    private float timerDeitado = 0.0f;

    void Start()
    {
        texto.enabled = true;
        Jogador = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timerDeitado += Time.deltaTime;

       if (timer < tempoExibicao && timerDeitado > tempoDeitado)
       {
            texto.enabled = true;
            timer += Time.deltaTime;
       }
       else
       {
            texto.enabled = false;
       }
    }
}
