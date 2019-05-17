using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectsActivator : MonoBehaviour {

    [SerializeField] private activationStatus action;
    [SerializeField] private GameObject[] objects;

    void Awake() {
        foreach (GameObject o in objects)
        {
            if (action == activationStatus.activate)
                o.SetActive(true);
            else
                o.SetActive(false);
        }
	}

    private enum activationStatus
    {
        activate,
        deactivate
    }
}
