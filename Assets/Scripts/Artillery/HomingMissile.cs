using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    GameObject[] targets;
    GameObject Player;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SelectTarget();
    }

    public void SelectTarget()
    {
        Vector3 playerPos = Player.transform.position;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < targets.Length; i++)
        {
            if (Vector3.Distance(playerPos, targets[i].transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(playerPos, targets[i].transform.position);
                target = targets[i];
            }
        }
    }
}
