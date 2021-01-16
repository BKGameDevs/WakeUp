using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Transform _PlayerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _PlayerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = transform.position;
        var playerPosition = _PlayerTransform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y + 1, cameraPosition.z);
    }
}
