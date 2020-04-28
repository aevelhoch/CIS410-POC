using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTriggerScript : MonoBehaviour
{
    cubeRotator parentCube;
    GameObject selfObject;
    public GameObject player;

    void Start()
    {
        parentCube = transform.parent.GetComponent<cubeRotator>();
        selfObject = transform.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            parentCube.InsideRotateTrigger(selfObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            parentCube.LeftRotateTrigger(selfObject);
        }
    }
}
