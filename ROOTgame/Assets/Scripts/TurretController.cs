using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject bulletPrefab;

    private float fireRate = 2f;

    [SerializeField] private GameObject gunBarrelRotationPoint;

    [SerializeField] private GameObject gunBarrelEnd;

    private Vector2 fireDir;

    private bool _allowFire = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AllowFire());
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerLocation = playerController.gameObject.transform.position;

        fireDir = playerLocation - (Vector2)this.gameObject.transform.position;

        gunBarrelRotationPoint.transform.up = -fireDir;

        if((Time.time > fireRate) && _allowFire)
        {

            fireRate = Time.time+1/0.5f;

            Shoot();

        }

    }

    void Shoot() 
    {

        GameObject bullet = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().AddForce(fireDir * 1f, ForceMode2D.Impulse);

        StartCoroutine(bulletDeath(bullet));

    }

    IEnumerator bulletDeath(GameObject bullet) {

        yield return new WaitForSeconds(2.5f);
        Destroy(bullet);

    }

    IEnumerator AllowFire() {

        yield return new WaitForSeconds(1.5f);
        _allowFire = true;

    }
}
