using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public PlayerMovement player;
    [SerializeField] private Vector3 offset = new Vector3(-5, 10, -5);
    
	void Update () {
        transform.position = player.transform.position + offset;
	}
}
