using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehaviours : MonoBehaviour {

    [SerializeField] private Color[] outfitColors;

	void Start () {
        int random = Random.Range(0, outfitColors.Length);
        GetComponent<Renderer>().material.color = outfitColors[random];
	}
	
	void Update () {
		
	}
}
