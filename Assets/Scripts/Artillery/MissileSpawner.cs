
using System.Collections;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missile;
    Cooldown volleyCooldown;

    public float volleyCooldownTime = 10;
    float launchCooldownTime = 0.5f;
    public int missilesPerVolley;
    public float volleyTime = 0.5f;


    private void Start()
    {
        volleyCooldown = new Cooldown(volleyCooldownTime);
    }

    private void Update()
    {
        if (!volleyCooldown.isCoolingDown)
        {
            volleyCooldown.StartCooldown();
            StartCoroutine(LaunchMissiles(missilesPerVolley));
        }
        volleyCooldown.SetCooldownTime(volleyCooldownTime);
    }

    public IEnumerator LaunchMissiles(int missiles)
    {
        while (missiles > 0)
        {
            Instantiate(missile, transform.position, Quaternion.identity);
            missiles--;
            yield return new WaitForSeconds(volleyTime / missilesPerVolley);
        }
    }
}
