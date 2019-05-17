using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{


    public Transform mainCamera;

    public Transform view;
    public Transform nearbyObjContr;

    [Range(0.1f, 5)]
    public float walkSpeed = 1.5f;
    [Range(0.1f, 1)]
    public float characterRotationSpeed = 0.5f;


    private float m_angle;
    Vector3 m_targetAngle;
    Vector3 m_currentAngle;


    private NavMeshAgent m_Agent;

    private Vector3 moveDirection = Vector3.zero;
    private bool lockRotation = false;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            transform.eulerAngles = new Vector3(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);

            Vector3 movement = new Vector3(h, 0, v);
            if (movement.magnitude > 1)
                movement = Vector3.Normalize(movement);

            float changeSpeed = lockRotation ? 0.15f : 0.2f;
            gameObject.transform.Translate(movement * walkSpeed * changeSpeed);

            if (lockRotation)
            {
                m_targetAngle = new Vector3(0f, nearbyObjContr.rotation.eulerAngles.y, 0f);
            }
            else
            {
                m_targetAngle = new Vector3(0f, m_angle, 0f);
            }

            m_angle = mainCamera.transform.rotation.eulerAngles.y + (-(Mathf.Atan2(v, h) * Mathf.Rad2Deg) + 90);

            m_currentAngle = new Vector3(
                Mathf.LerpAngle(m_currentAngle.x, m_targetAngle.x, characterRotationSpeed),
                Mathf.LerpAngle(m_currentAngle.y, m_targetAngle.y, characterRotationSpeed),
                Mathf.LerpAngle(m_currentAngle.z, m_targetAngle.z, characterRotationSpeed));

            view.eulerAngles = m_currentAngle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tool")
        {
            lockRotation = true;
            other.GetComponent<Prototype>().showUi = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "tool")
        {
            lockRotation = false;
            other.GetComponent<Prototype>().showUi = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "tool")
        {
            nearbyObjContr.LookAt(other.transform);
        }
    }
}
