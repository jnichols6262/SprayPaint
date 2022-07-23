using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class sSprayPaintOnGround : MonoBehaviour
{
    public GameObject pPaint;
    public Transform paintSpawn;
    SprayPaint sprayPaint;
    public GameManager gm;
   
    public sMovement movement;

    private void Awake()
    {
        gm = GameManager.gm;

        ControllerManager.controllerManager.playerControls.Gameplay.SprayOnGround.Enable();
        ControllerManager.controllerManager.playerControls.Gameplay.SprayOnGround.performed += OnPaintPress;
        ControllerManager.controllerManager.playerControls.Gameplay.SprayOnGround.canceled += OnPaintRelease;
    }
    void OnPaintPress(InputAction.CallbackContext context)
    {
        if (movement.isGrounded == true)
        {
            gm = GameManager.gm;
            gm.s_Audio.Play(2);
            GameObject newPaint = Instantiate(pPaint, paintSpawn.position, Quaternion.identity);
            sprayPaint = newPaint.GetComponent<SprayPaint>();
            
        }
        else
            sprayPaint = null;
        
    }

    void OnPaintRelease(InputAction.CallbackContext context)
    {
        sprayPaint = null;
        gm.s_Audio.Stop(2);
        
    }
    private void Update()
    {
        if (sprayPaint != null)
        {
            Vector3 sprayPos = paintSpawn.position;
            sprayPaint.UpdateLine(sprayPos);

            
        }

        if (movement.isGrounded == false)
        {
            sprayPaint = null;
            
        }

    }

}

