using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OrdersController : MonoBehaviour {

    [SerializeField] private float hOffset = 20;
    [SerializeField] private GameObject uiElement;
    [SerializeField] private GameObject nearbyObjContr;

    [HideInInspector]
    public bool showUi = false;
    private float scale = 0.0f;
    
    private Vector3 m_targetAngle;
    private Vector3 m_currentAngle;

    private void Start()
    {
        uiElement.transform.localScale = Vector3.zero;
    }

    void LateUpdate()
    {

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

        uiElement.transform.localScale = new Vector3(scale, scale, scale);

        if (scale > 0.0f)
        {
            Vector3 tempPos = transform.position;
            tempPos.y += hOffset;
            tempPos = Camera.main.WorldToScreenPoint(tempPos);
            uiElement.transform.position = tempPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "client")
            showUi = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "client")
            showUi = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "client")
        {
            nearbyObjContr.transform.position = other.transform.position;
            nearbyObjContr.transform.LookAt(gameObject.transform.position);

            m_targetAngle = new Vector3(0f, nearbyObjContr.transform.rotation.eulerAngles.y, 0f);

            m_currentAngle = new Vector3(
                Mathf.LerpAngle(other.transform.eulerAngles.x, m_targetAngle.x, 0.1f),
                Mathf.LerpAngle(other.transform.eulerAngles.y, m_targetAngle.y, 0.1f),
                Mathf.LerpAngle(other.transform.eulerAngles.z, m_targetAngle.z, 0.1f));

            other.gameObject.transform.eulerAngles = m_currentAngle;
        }
    }

}
