using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srcTPC : MonoBehaviour
{
    // Variaveis publicas
    public float velocGiro = 4f;
    public float minAngGiro = -90f;
    public float maxAngGiro = 90f;

    public GameObject alvo;

    // variaveis de processamento
    private float distAlvo;
    private float rotX;

    void Start()
    {
        distAlvo = Vector3.Distance(transform.position, alvo.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Translacao do personagem: teclado
        ControleTeclado();

        // Translacao da cabeca: mouse
        ControleMouse();
    }

    void ControleTeclado()
    {

    }

    void ControleMouse()
    {
        // coletando informacoes do mouse
        float y = Input.GetAxis("Mouse X") * velocGiro;
        rotX += Input.GetAxis("Mouse Y") * velocGiro;

        // limitar o giro em torno do eixo y
        rotX = Mathf.Clamp(rotX, minAngGiro, maxAngGiro);

        // rotaciona a camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);

        // mover a camera
        transform.position = alvo.transform.position - (transform.forward * distAlvo);
    }
}
