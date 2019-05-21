using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueController : MonoBehaviour {

    private int clientsCount = 0;
    private List<GameObject> visibleClients = new List<GameObject>();

    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private Transform clientSpawn;
    [SerializeField] private Transform[] queuePlaces;
    

    void Start () {
        StartCoroutine(AddNewClient());
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject temp = visibleClients[0];
            visibleClients.RemoveAt(0);
            temp.GetComponent<NavMeshAgent>().SetDestination(clientSpawn.position);
            Destroy(temp, 3.0f);
            SetClientsPositions(true);
        }
    }

    private void CreateNewClient()
    {
        clientsCount++;
        Debug.Log("VC.Count: " + visibleClients.Count);
        Debug.Log("QP.Length: " + queuePlaces.Length);
        if (visibleClients.Count < queuePlaces.Length)
            visibleClients.Add(Instantiate(clientPrefab, clientSpawn.transform.position, Quaternion.identity) as GameObject);

        SetClientsPositions(true);
    }

    private void SetClientsPositions(bool allClients)
    {
        for (int i = 0; i < visibleClients.Count; i++)
        {
            SetClientsPositions(i);
        }
    }

    private void SetClientsPositions(int clientNo)
    {
        visibleClients[clientNo].GetComponent<NavMeshAgent>().SetDestination(queuePlaces[clientNo].position);
    }

    IEnumerator AddNewClient()
    {
        while (true)
        {
            CreateNewClient();
            yield return new WaitForSeconds(3);
        }
    }
}