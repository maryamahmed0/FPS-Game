using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public Camera cam;
    float Rotation=0f;
    public float xSens = 30f;
    public float ySens = 30f;
     
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        Rotation-=(mouseY*Time.deltaTime)*ySens;
        Rotation=Mathf.Clamp(Rotation,-80f,80f);
        cam.transform.localRotation = Quaternion.Euler(Rotation,0,0);
        transform.Rotate(Vector3.up*(mouseX*Time.deltaTime)*xSens);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
