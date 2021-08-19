using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Components")]
    public CharacterController controller;
    public Transform cam;
    public InputHandler inputs;
    public AnimationHandler animatonHandler;
    public PlayerGetHit playerHit;

    [Header("Movement Stats")]
    public float walkSpeed = 3f; //This is your speed when you are walking
    public float runSpeed = 6f; //This is your speed when you are running
    float speed;
    public float delta;

    public float turnSmoothtime = 0.1f; //This is the smoothness of the rotation of the player
    float turnSmoothVelocity;
    [HideInInspector]
    public bool isMoving; //This is a bool to check if you can move or not
    private bool groundedPlayer; //This is to check if the player is grounded
    private float gravityValue = -9.81f; //This is the value of the gravity the affects the player
    private Vector3 playerVelocity; //This is for controlling the player in the y axis, this can be used to make the player jump in the future is necessarry

    [Header("Magics")]
    public bool GotMagic1; //This is to check if the player got the Magic 1
    public bool GotMagic2; //This is to check if the player got the Magic 2

    [Header("RPG Values")]
    public int HP = 8; //This is the HP of the player
    public bool isDefending; //This is to check if the player is defending

    [SerializeField] private bool LockCursor = false;                   // Whether the cursor should be hidden and locked.

    // Start is called before the first frame update
    void Start()
    {
        animatonHandler.Initialize();
        HP = 8;
        Cursor.lockState = LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !LockCursor;
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;
        inputs.TickInput(delta);
        MovementHandler(delta);
        GroundedCheck();
        PlayerGravity();
        DefenseHandler();
        AttackMagic1();
        AttackMagic2();
    }

    public void MovementHandler(float delta)
    {
        if (isMoving == true)
            return;

        float horizontal = inputs.horizontal;
        float vertical = inputs.vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputs.moveAmount < 0.5)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = runSpeed;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * delta);
        }

        animatonHandler.UpdateAnimatorValues(inputs.moveAmount, 0);
    }

    private void GroundedCheck()
    {
        groundedPlayer = controller.isGrounded;
    }

    private void PlayerGravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void DefenseHandler()
    {
        if (animatonHandler.anime.GetBool("Def") == true)
        {
            isDefending = true;
        }
        else if (animatonHandler.anime.GetBool("Def") == false)
        {
            isDefending = false;
        }

        if (inputs.defense_Input == true && isMoving == false)
        {
            animatonHandler.PlayAnimation("Def", 1.0f);
        }
    }

    public void AttackMagic1()
    {
        if (inputs.attack1_Input == true && isMoving == false)
        {
            animatonHandler.PlayAnimation("Atk1", 0.40f);
        }
    }

    public void AttackMagic2()
    {
        if (inputs.attack2_Input == true && isMoving == false)
        {
            animatonHandler.PlayAnimation("Atk2", 1.0f);
        }
    }
}
