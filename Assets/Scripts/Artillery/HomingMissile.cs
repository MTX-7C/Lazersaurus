using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    GameObject[] targets;
    GameObject Player;
    Rigidbody2D rb;
    public float missileSpeed;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectTarget();
        rb.linearVelocity = transform.right * missileSpeed;
        Debug.Log(rb.linearVelocity);
        AimAtTarget();
    }

    public void AimAtTarget()
    {
        float desiredAngle;
        float jitterAmount = Random.Range(-10, 10);
        float x = 0;
        desiredAngle = Mathf.Atan2((target.transform.position.y - transform.position.y), (target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg + jitterAmount;
        transform.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.z, desiredAngle, ref x, 0.04f));
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
