using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public Transform playerCarmera;
    public Vector3 l;
    private void Update()
    {
        l = playerCarmera.position - door1.position;
        this.transform.position = door2.position + l;
    }
}
