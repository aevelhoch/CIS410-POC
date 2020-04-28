using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControlUsingCharController : MonoBehaviour
{
    public float jumpPower;
    public float selfGravity;
    public float moveSpeed;
    public int numJumps;

    Vector3 m_Movement;
    CharacterController m_CharController;
    Transform m_Transform;
    Collider m_Collider;
    Renderer m_Renderer;

    bool headBonked = false;
    bool sideBonked = false;
    int currentJumps;
    bool jumpKeyUp = true;
    float verticalSpeed = 0;
    float horizSpeed = 0;
    bool facingRight = true;
    bool prevFacingRight = true;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_CharController = GetComponent<CharacterController>();
        m_Collider = GetComponent<BoxCollider>();
        m_Renderer = GetComponent<Renderer>();
        currentJumps = numJumps;
    }

    void Update()
    {
        // reset if the r key is pressed
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // first, check grounded things: reset jumps, movement, reset bonked
        if (m_CharController.isGrounded)
        {
            headBonked = false;
            sideBonked = false;
            currentJumps = numJumps; // reset jumps if grounded
            // can only change x direction at any time when grounded
            if (Input.GetKey(KeyCode.A) && m_CharController.isGrounded)
            {
                horizSpeed = -moveSpeed;
                facingRight = false;
            }
            else if (Input.GetKey(KeyCode.D) && m_CharController.isGrounded)
            {
                horizSpeed = moveSpeed;
                facingRight = true;
            }
            else if (m_CharController.isGrounded) horizSpeed = 0;
        }

        // if the player is in air, apply gravity
        if (!m_CharController.isGrounded)
        {
            verticalSpeed -= selfGravity * Time.deltaTime;
        }
        else // otherwise if theyre on ground, don't increase gravity, but apply it
        {
            verticalSpeed = -selfGravity * Time.deltaTime;
        }

        // jump if there are remaining jumps and the key is not held down
        if (Input.GetButtonDown("Jump") && currentJumps > 0 && jumpKeyUp == true)
        {
            headBonked = false;
            sideBonked = false;
            // can also change x dir in air while jumping
            if (Input.GetKey(KeyCode.A))
            {
                horizSpeed = -moveSpeed;
                facingRight = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizSpeed = moveSpeed;
                facingRight = true;
            }
            else horizSpeed = 0;
            // reset gravity when jumping in midair
            verticalSpeed = jumpPower;
            currentJumps--;
            jumpKeyUp = false;
        }
        // reset jumpKeyUp when player releases key
        if (Input.GetButtonUp("Jump")) jumpKeyUp = true;

        // finally, check our two types of 'bonks'
        // hit head on ceiling bonk
        if (((m_CharController.collisionFlags & CollisionFlags.Above) != 0) 
            && !headBonked && verticalSpeed > 0)
        {
            //start going down again
            headBonked = true;
            verticalSpeed = -selfGravity * Time.deltaTime;
        }
        // hit side on wall bonk
        if (((m_CharController.collisionFlags & CollisionFlags.Sides) != 0)
            && !sideBonked && !m_CharController.isGrounded)
        {
            sideBonked = true;
            horizSpeed = 0;
        }

        // finally set and apply the movement to the player object
        m_Movement.Set(horizSpeed, verticalSpeed, 0f);
        m_CharController.Move(m_Movement * Time.deltaTime);

        // flip the gameObject so the texture faces the other way if we changed direction
        if ((facingRight && !prevFacingRight) || (!facingRight && prevFacingRight))
        {
            //some sort of character flip code goes here
        }
        // and set current facing to prev facing for next iteration
        prevFacingRight = facingRight;
    }

    public float getVerticalSpeed()
    {
        return verticalSpeed;
    }
}
