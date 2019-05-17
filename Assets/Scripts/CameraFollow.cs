using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public PlayerMovement player;
    [SerializeField] private Vector3 offset = new Vector3(-5, 10, -5);
    [SerializeField] private bool lockX = false;
    [SerializeField] private bool lockY = false;
    [SerializeField] private bool lockZ = false;

    void Update () {
        Vector3 tempPosition = Vector3.zero;
        tempPosition.x = lockX ? offset.x : player.transform.position.x + offset.x;
        tempPosition.y = lockY ? offset.y : player.transform.position.y + offset.y;
        tempPosition.z = lockZ ? offset.z : player.transform.position.z + offset.z;

        transform.position = tempPosition;

        transform.LookAt(player.gameObject.transform.position);
	}
}
