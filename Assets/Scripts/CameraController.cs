using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraY = 4f;
    public float cameraZ = -9.21f;
    [SerializeField] private Transform player;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

    }
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + cameraY, cameraZ);
    }
}
