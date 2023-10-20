using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterTrapController : MonoBehaviour
{
    public float RotationSpeed;
    public GameObject Projectile;
    public float ProjectileSpeed;
    private float ShootCooldown;

    // Start is called before the first frame update
    void Start()
    {
        ShootCooldown = Time.time;
    }

    public void Patrol()
    {
        transform.Rotate(new Vector3(transform.rotation.x, 30, transform.rotation.z), RotationSpeed / 6);
    }

    public bool Aim(GameObject target)
    {
        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(targetRotation, transform.rotation) <= 30)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Shoot(GameObject target)
    {
        transform.LookAt(target.transform.position);

        if (Time.time >= ShootCooldown)
        {
            var newProjectile = Instantiate(Projectile, transform.Find("AXLE 40MM").position, transform.rotation);

            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * ProjectileSpeed);

            ShootCooldown = Time.time + 1;
        }
    }
}
