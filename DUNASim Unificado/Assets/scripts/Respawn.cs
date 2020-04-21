using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Respawn Faz o Objeto ao qual foi atribuído voltar 
// a posição inicial ao ser desativado e reativado

public class Respawn : MonoBehaviour
{
    Vector3 SpawnPoint;
    Quaternion OriginalRotation;

    private void Awake()
    {
        SpawnPoint = transform.position;
        OriginalRotation = transform.rotation;
    }

    private void OnDisable()
    {
        transform.position = SpawnPoint;
        transform.rotation = OriginalRotation;
    }
}
