using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float jumpPower;
    public float selfGravity;
    public float moveSpeed;
    public int numJumps;

    Vector3 m_Movement;
    Rigidbody m_Rigidbody;
    Transform m_Transform;

    int currentJumps;
    bool grounded = false;
    bool jumpKeyUp = true;
    float verticalSpeed = 0;
    float horizSpeed = 0;
    bool facingRight = true;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody>();
        currentJumps = numJumps;
    }

    void FixedUpdate()
    {
        print($"grounded by collion: {grounded}");
        // can only change x move while standing on ground (or jumping)
        if (Input.GetKey(KeyCode.A) && grounded)
        {
            facingRight = false;
            horizSpeed = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D) && grounded)
        {
            facingRight = true;
            horizSpeed = moveSpeed;
        }
        else if (grounded) horizSpeed = 0;

        // jump if there are remaining jumps and the key is not held down
        if (Input.GetButtonDown("Jump") && currentJumps > 0 && jumpKeyUp == true)
        {
            // can also change x dir in air while jumping
            if (Input.GetKey(KeyCode.A))
            {
                facingRight = false;
                horizSpeed = -moveSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                facingRight = true;
                horizSpeed = moveSpeed;
            }
            else horizSpeed = 0;
            // reset gravity when jumping in midair
            verticalSpeed = jumpPower;
            currentJumps--;
            jumpKeyUp = false;
            grounded = false;
        }
        // reset jumpKeyUp when player releaseskey
        if (Input.GetButtonUp("Jump")) jumpKeyUp = true;

        // if the player is not in the ground, decrease v move by gravity amount
        if(!grounded)
        {
            verticalSpeed -= selfGravity;
        }
        else // otherwise if theyre on ground, don't apply gravity
        {
            verticalSpeed = 0;
        }

        
        // swap player object facing if they turn around
        if (facingRight)
        {
            m_Rigidbody.MoveRotation(Quaternion.Euler(0f, 180f, 0f));
        }
        else m_Rigidbody.MoveRotation(Quaternion.identity);
        

        // finally set and apply the movement to the player object
        m_Movement.Set(horizSpeed, verticalSpeed, 0f);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentJumps = numJumps;
        grounded = true;
        /*if (m_Rigidbody.position.y < .5)
        {
            m_Movement.Set(m_Rigidbody.position.x, 0.5f, m_Rigidbody.position.z);
            m_Rigidbody.MovePosition(m_Movement);
        }*/
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
