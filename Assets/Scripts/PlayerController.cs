using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Object components
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rbody;
    private Animator anim;
    private LineRenderer lineRenderer;

    // Movement
    public float speed = 64;
    public Vector2 lastMove;
    private bool movementEnabled = true;

    // Adjusting parameters
    private const float spriteOffsetBeforeCollidingScreenEdgeLeft = 12;
    private const float spriteOffsetBeforeCollidingScreenEdgeRight = -12;
    private const float spriteOffsetBeforeCollidingScreenEdgeTop = -12;
    private const float spriteOffsetBeforeCollidingScreenEdgeBottom = -12;

    private float getRelativeX()
    {
        return rbody.position.x;
    }

    private float getRelativeY()
    {
        return rbody.position.y;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movementEnabled)
        {
            checkMovement();
        }
    }

    private void checkMovement()
    {
        move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), numberOfPixelsToMoveInOneFrame());
    }

    private void move(Vector2 movementVector, float numberOfPixelsToMove)
    {
        if (checkMapBounds(movementVector))
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("inputX", movementVector.x);
            anim.SetFloat("inputY", movementVector.y);
            rbody.MovePosition(rbody.position + numberOfPixelsToMove * movementVector);
            lastMove = movementVector;
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private bool checkMapBounds(Vector2 movementVector)
    {
        bool canWalk = false;
        if (movementVector != Vector2.zero)
        {
            canWalk = true;
            /*if ((movementVector.x < 0) && (getRelativeX() < (0 + spriteOffsetBeforeCollidingScreenEdgeLeft)))
            {
                canWalk = false;
            }
            if ((movementVector.y > 0) && (getRelativeY() > (0 + spriteOffsetBeforeCollidingScreenEdgeTop)))
            {
                canWalk = false;
            }
            if ((movementVector.x > 0) && (getRelativeX() > (map.GetMapWidthInPixelsScaled() + spriteOffsetBeforeCollidingScreenEdgeRight)))
            {
                canWalk = false;
            }
            if ((movementVector.y < 0) && (getRelativeY() < -(map.GetMapHeightInPixelsScaled() + spriteOffsetBeforeCollidingScreenEdgeBottom)))
            {
                canWalk = false;
            }*/
        }
        return canWalk;
    }

    private float numberOfPixelsToMoveInOneFrame()
    {
        return speed * Time.deltaTime;
    }

}