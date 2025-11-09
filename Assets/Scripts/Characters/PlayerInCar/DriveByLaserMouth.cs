using UnityEngine;

public class DriveByLaserMouth : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint;

    Animator animator;

    public float bulletForce = 1500.0f;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("DriveByFire", true);
            FireBullet();
        }
        else
        {
            animator.SetBool("DriveByFire", false);
        }
    }

    void FireBullet()
    {
        // Bullet instantiate at the position of GameObject
        GameObject newBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation) as GameObject;

        // get Rigidbody2D component of instantiated Bullet
        Rigidbody2D tempRigidBody = newBullet.GetComponent<Rigidbody2D>();

        // push the Bullet forward by amount bulletForce
        // fireForward is fire to the right
        tempRigidBody.AddForce(Vector2.right * bulletForce);

        // basic Clean Up, set Bullets to self destruct after 2 seconds
        Destroy(newBullet, 2.0f);
    }
}
