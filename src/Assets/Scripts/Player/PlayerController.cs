using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Camera cam;
    Player player;

    //Movement Values
    [SerializeField] float moveSpeed = 15;
    Vector2 moveDir;
    float moveDrag = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    void Start()
    {
        cam = Camera.main;
        rb.drag = moveDrag; 
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveDir.x, 0, moveDir.y).normalized * moveSpeed * 10, ForceMode.Force);
        ClampSpeed();
    }

    void ClampSpeed()
    {
        Vector3 vel = rb.velocity;
        if (vel.magnitude > moveSpeed)
        {
            Vector3 clampedVel = vel.normalized * moveSpeed;
            rb.velocity = new Vector3(clampedVel.x, rb.velocity.y, clampedVel.z);
        }
    }

    public void SelectTrinket(int selection)
    {
        player.SelectTrinket(selection);
    }

    public void DoClick()
    {
        if (player.currentState == PlayerState.Trinket)
        {
            player.Purchase();
        }
        else if (player.currentState == PlayerState.Normal)
        {
            player.ViewTrinket(cam);
        }
    }
    public void ToggleTrinkets()
    {
        player.TriggerNormalMode();
    }

    public void DoMovement(Vector2 value)
    {
        moveDir = value;
    }

    public void UpdateCursorPos(Vector2 mousePos)
    {
        player.mouseRay = cam.ScreenPointToRay(mousePos);

    }
}
