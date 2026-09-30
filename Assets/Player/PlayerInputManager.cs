using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // using Player Input component.
    protected float mouseSens = 100f;
    public Transform playerBody;

    void Start()
    {
        playerBody = transform;
    }

    void Update()
    {
        
    }

    public void OnJump()
    {
        UnityEngine.Debug.Log("jumped");
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        UnityEngine.Debug.Log(v);
    }
    
}
