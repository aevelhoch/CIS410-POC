using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passThruPlatform : MonoBehaviour
{
    public GameObject player;
    BoxCollider m_Collider;

    private void Start()
    {
        m_Collider = transform.parent.GetComponentInParent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            m_Collider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            m_Collider.enabled = true;
        }
    }
}
