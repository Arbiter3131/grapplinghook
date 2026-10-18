using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // handles getting inputs from the player, everything in here needs to be public for the inputmanager to work properly.

    PlayerInputManager input;
    PlayerController controller;

    void Start()
    {
        input = transform.GetComponent<PlayerInputManager>();
        controller = transform.GetComponent<PlayerController>();
    }

    // if mouse is moved, this function gets called.
    public void OnLook(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        controller.MoveCamera(v);
    }

    public void OnJump()
    {
        UnityEngine.Debug.Log("jumped");
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        controller.PlayerMovement(v);
    }
}
