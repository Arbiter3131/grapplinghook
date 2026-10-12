// using UnityEngine;

// public class PlayerMovement : GlobalCharacter
// {
//     private Vector3 velocity = new Vector3(0,0,0);
//     private Vector3 acceleration = new Vector3(0,0,0);

//     // Max velocity is the maximum velocity achievable in any direction. This means you could go diagonally
//     // with 5.00 velocity forwards and 5.00 velocity to the left. with a total of 10.00 velocity.
//     protected float maxVelocity = 0.01f;
//     protected float moveAcceleration = 1.00f;

//     // air resistance slowing your velocity in air.
//     protected float airResistance = 0.25f;

//     // friction slowing your velocity on the ground.
//     protected float friction = 0.50f;

//     // instantly added to your upwards velocity.
//     protected float jumpAcceleration = 2.00f;
//     // prevent 
//     protected bool jumping = false;

//     // gravity.
//     protected float gravityAcceleration = 0.01f;

//     protected PlayerInputManager input;

//     // camera controls, sensitivity and the vertical component of the camera.
//     float lookSens = 0.5f;
//     float upDownLook = 0f;
//     Camera camera;

//     // below are inherited from GlobalCharacter.
//     // parentPawn = transform.gameobject
//     // rigidbody = rigidbody

//     void Start()
//     {
//         input = transform.GetComponent<PlayerInputManager>();
//         camera = transform.GetComponentInChildren<Camera>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     // Called by PlayerInputManager to handle camera movement.
//     // Passed the Vector2 from the input.
//     // Rotates the player directly by the x input of the mouse movement * looksensitivity to control how fast the camera moves.
//     // upDownLook tracks the vertical camera look, with a maximum and minimum of -90 to 90 so that the camera doesn't flip.
//     // then the cameras local rotation is set to the value of upDownLook.
//     public void CameraMovement(Vector2 v)
//     {
//         float leftRight = v.x * lookSens;
//         transform.Rotate(Vector3.up * leftRight);

//         upDownLook -= v.y * lookSens;
//         upDownLook = Mathf.Clamp(upDownLook, -90f, 90f);
        
//         camera.transform.localRotation = Quaternion.Euler(upDownLook, 0f, 0f);
//     }

    

//     private void VerticalVelocity()
//     {
//         CheckGrounded();
//         if (!grounded && math.abs(vertVel) < maxVelocity)
//         {
//             vertVel -= gravityAcceleration;
//         }
//         else if (grounded)
//         {
//             vertVel = 0.0f;
//         }
//     }

//     void Start()
//     {
//         pMove = transform.GetComponent<PlayerMovement>();
//     }

//     public void OnJump()
//     {
//         UnityEngine.Debug.Log("jumped");
//     }

//     public void OnMove(InputValue value)
//     {
//         var v = value.Get<Vector2>();
//         UnityEngine.Debug.Log(v);
//     }
    

//     // gets mouselook input, then calls cameramovement in PlayerMovement.
//     public void OnLook(InputValue value)
//     {
//         Vector2 v = value.Get<Vector2>();
//         pMove.CameraMovement(v);
//     }
// }
