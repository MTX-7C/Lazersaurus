using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    Player player;
    public GameObject originPoint;
    public float maxDistance = 20;

    public LayerMask laserMask;

    public float damage = 3;
    public float ticksPerSecond = 5;

    public bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, originPoint.transform.position);

        if (active)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, GetTargetPoint());
        } else
        {
            lineRenderer.positionCount = 0;
        }
    }

    public Vector3 GetTargetPoint()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(originPoint.transform.position, transform.right * player.facedDirection, maxDistance, laserMask);
        if (hit.collider != null)
        {
            return hit.point;
        } else
        {
            Debug.Log(originPoint.transform.position + transform.right * player.facedDirection * maxDistance);
            return originPoint.transform.position + transform.right * player.facedDirection * maxDistance;
        }
    }
    public void DamageEnemy()
    {

    }
}
