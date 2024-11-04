using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srcFPC : MonoBehaviour
{
  public float sensX;
  public float sensY;
  
  public Transform orientation;

  float xRotation;
  float yRotation;

    // Inicia o script
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    // Atualiza a cada frame
    void Update()
    {
      float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
      float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

      yRotation += mouseX;
      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -80f, 45f);
      transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
      orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
