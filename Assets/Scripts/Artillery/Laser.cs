using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    public AudioClip damageSound;
    Player player;
    public GameObject originPoint;
    public float maxDistance = 20;

    public LayerMask laserMask;

    public float damage = 3;
    public float ticksPerSecond = 5;
    float t = 0;
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
        

        if (active)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, GetTargetPoint());
            lineRenderer.SetPosition(0, originPoint.transform.position);
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
            if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                DamageEnemy(hit.collider.gameObject.GetComponent<IDamageable>());
            }
            return hit.point;
        } else
        {
            Debug.Log(originPoint.transform.position + transform.right * player.facedDirection * maxDistance);
            return originPoint.transform.position + transform.right * player.facedDirection * maxDistance;
        }
    }
    public void DamageEnemy(IDamageable enemy)
    {
        t += Time.deltaTime;

        if (t >= 1f / ticksPerSecond)
        {
            t = 0;
            enemy.Damage(damage);
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
    }
}
