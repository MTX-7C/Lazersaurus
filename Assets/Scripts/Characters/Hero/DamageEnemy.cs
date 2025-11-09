using UnityEngine;

public class DamageEnemy : MonoBehaviour
{

    public AudioClip damageSound;
    public float damage = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<IDamageable>().Damage(damage);
            AudioSource.PlayClipAtPoint(damageSound, transform.position);

        }
    }
}
