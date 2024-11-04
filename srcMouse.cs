using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srcMouse : MonoBehaviour
{

    //Variaveis publicas
    public float velocHor = 1f, velocVer = 1f;

    //Variaveis de processamento
    private float rotX = 0f, rotY = 0f;
    private Camera cabeca;
    void Start()
    {
        cabeca = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Mouse X") * velocHor;
        float movY = Input.GetAxis("Mouse Y") * velocVer;

        //Definir rotações da cabeça
        rotX -= movY;
        rotY += movX;

        //Limitar movimento da cabeça para cima e para baixo
        rotX = Mathf.Clamp(rotX, -60f, 90);

        cabeca.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
    }
}
