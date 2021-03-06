﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Prototype : MonoBehaviour {

    [SerializeField] private float hOffset = 20;
    [SerializeField] private GameObject uiElement;
    private bool conectedWithOrder = false;
    private OrdersController ordersController;

    [HideInInspector]
    public bool showUi = false;
    private float scale = 0.0f;

    private void Start()
    {
        uiElement.transform.localScale = Vector3.zero;
        if(GetComponent<OrdersController>() != null)
        {
            conectedWithOrder = true;
            ordersController = GetComponent<OrdersController>();
        }
    }

    void LateUpdate () {

        if (showUi)
        {
            scale += Time.deltaTime * 10;
            if (scale > 1.0f) scale = 1.0f;
        }
        else
        {
            scale -= Time.deltaTime * 10;
            if (scale < 0.0f) scale = 0.0f;
        }

        if (!conectedWithOrder)
            uiElement.transform.localScale = new Vector3(scale, scale, scale);
        else
            uiElement.transform.localScale = 
                new Vector3(scale*ordersController.scale,
                    scale * ordersController.scale,
                    scale * ordersController.scale);

        if (scale > 0.0f)
        {
            Vector3 tempPos = transform.position;
            tempPos.y += hOffset;
            tempPos = Camera.main.WorldToScreenPoint(tempPos);
            uiElement.transform.position = tempPos;
        }
	}
}
