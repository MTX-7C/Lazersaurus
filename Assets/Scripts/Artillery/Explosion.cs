using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float despawnTime = 1f;
    float hitboxTime = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        despawnTime -= Time.deltaTime;
        hitboxTime -= Time.deltaTime;
        if (despawnTime < 0)
        {
            Destroy(gameObject);
        }
        if (hitboxTime < 0)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
