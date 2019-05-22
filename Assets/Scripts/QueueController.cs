using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class QueueController : MonoBehaviour {

    private int clientsCount = 0;
    private List<GameObject> visibleClients = new List<GameObject>();

    [SerializeField] private Text clientsOutsideText;
    [SerializeField] private OrdersController ordersController;
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private Transform clientSpawn;
    [SerializeField] private Transform[] queuePlaces;


    void Start () {
        StartCoroutine(AddNewClient());
        ordersController.queueController = this;
	}

    private void Update()
    {
        int temp = clientsCount - visibleClients.Count;
        if (temp < 0) temp = 0;

        clientsOutsideText.text = "clients outside: " + temp;

        if (Input.GetKeyDown(KeyCode.F))
        {
            NextClient();
        }
    }

    private void CreateNewClient()
    {
        clientsCount++;
        if (visibleClients.Count < queuePlaces.Length)
            SpawnClient();

        SetClientsPositions(true);
    }

    private void SpawnClient()
    {
        visibleClients.Add(Instantiate(clientPrefab, clientSpawn.transform.position, Quaternion.identity) as GameObject);
    }

    public void NextClient()
    {
        clientsCount--;
        GameObject temp = visibleClients[0];
        visibleClients.RemoveAt(0);
        temp.GetComponent<NavMeshAgent>().SetDestination(clientSpawn.position);
        Destroy(temp, 3.0f);
        if (clientsCount > visibleClients.Count)
            SpawnClient();
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