using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeRotator : MonoBehaviour
{
    enum Dim
    {
        Xplus,Xminus,Yplus,Yminus,Zplus,Zminus
    }

    public GameObject player;
    public GameObject stage;
    public GameObject RotateTriggerXYsamesign;
    public GameObject RotateTriggerYZsamesign;
    public GameObject RotateTriggerZXsamesign;
    public GameObject RotateTriggerXYdiffsign;
    public GameObject RotateTriggerYZdiffsign;
    public GameObject RotateTriggerZXdiffsign;

    Vector3 m_Facing;
    Vector3 m_Up;
    playerControlUsingCharController m_Player;

    bool rightRotOk = false;
    bool leftRotOk = false;
    bool upRotOk = false;
    bool downRotOk = false;

    private void Start()
    {
        m_Facing = Vector3.forward;
        m_Up = Vector3.up;
        m_Player = player.GetComponent<playerControlUsingCharController>();
    }

    public void InsideRotateTrigger(GameObject rotateTrigger)
    {
        if (m_Facing == Vector3.forward && rotateTrigger == RotateTriggerZXdiffsign && m_Up == Vector3.up)
        {
            rightRotOk = true;
        }
        else if (m_Facing == Vector3.left && rotateTrigger == RotateTriggerZXdiffsign)
        {
            leftRotOk = true;
        }
        else if (m_Facing == Vector3.left && rotateTrigger == RotateTriggerXYsamesign)
        {
            upRotOk = true;
        }
        else if (m_Facing == Vector3.down && rotateTrigger == RotateTriggerXYsamesign)
        {
            downRotOk = true;
        }
        else if (m_Facing == Vector3.down && rotateTrigger == RotateTriggerYZdiffsign)
        {
            leftRotOk = true;
        }
        else if (m_Facing == Vector3.forward && rotateTrigger == RotateTriggerYZdiffsign)
        {
            rightRotOk = true;
        }
        else if (m_Facing == Vector3.forward && rotateTrigger == RotateTriggerZXdiffsign && m_Up == Vector3.left)
        {
            downRotOk = true;
        }
    }

    public void LeftRotateTrigger(GameObject rotateTrigger)
    {
        rightRotOk = false;
        leftRotOk = false;
        upRotOk = false;
        downRotOk = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) && rightRotOk)
        {
            RotateRight();
        } else if (Input.GetKey(KeyCode.A) && leftRotOk)
        {
            RotateLeft();
        } else if (m_Player.getVerticalSpeed() > 0 && upRotOk)
        {
            RotateUp();
        } else if (m_Player.getVerticalSpeed() < 0 && downRotOk)
        {
            RotateDown();
        }
    }

    void RotateRight()
    {
        rightRotOk = false;
        player.gameObject.SetActive(false);
        stage.transform.Rotate(0f, 90f, 0f, Space.World);
        player.transform.position = Vector3.Reflect(player.transform.position, Vector3.left);
        if (m_Up == Vector3.up) m_Facing = Vector3.left;
        else if (m_Up == Vector3.left) m_Facing = Vector3.down;
        player.gameObject.SetActive(true);
    }

    void RotateLeft()
    {
        leftRotOk = false;
        player.gameObject.SetActive(false);
        stage.transform.Rotate(0f, -90f, 0f, Space.World);
        player.transform.position = Vector3.Reflect(player.transform.position, Vector3.right);
        if (m_Up == Vector3.up) m_Facing = Vector3.forward;
        else if (m_Up == Vector3.left) m_Facing = Vector3.forward;
        player.gameObject.SetActive(true);
    }

    void RotateUp()
    {
        upRotOk = false;
        player.gameObject.SetActive(false);
        stage.transform.Rotate(-90f, 0f, 0f, Space.World);
        player.transform.position = Vector3.Reflect(player.transform.position, Vector3.up);
        m_Facing = Vector3.down;
        m_Up = Vector3.left;
        player.gameObject.SetActive(true);
    }

    void RotateDown()
    {
        downRotOk = false;
        player.gameObject.SetActive(false);
        stage.transform.Rotate(90f, 0f, 0f, Space.World);
        player.transform.position = Vector3.Reflect(player.transform.position, Vector3.up);
        m_Facing = Vector3.left;
        m_Up = Vector3.up;
        player.gameObject.SetActive(true);
    }
}
