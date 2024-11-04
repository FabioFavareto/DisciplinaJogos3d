using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scriptTravelMessage : MonoBehaviour
{
    public TextMeshProUGUI texto;
    [Range(0.1f, 10.0f)] public float distancia = 3;
    private GameObject Jogador;

    void Start()
    {
        texto.enabled = false;
        Jogador = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Jogador.transform.position) < distancia)
        {
            texto.enabled = true;
        }
        else
        {
            texto.enabled = false;
        }
    }
}
