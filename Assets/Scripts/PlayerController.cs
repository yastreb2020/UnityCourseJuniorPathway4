using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 5;
    private float powerUpStrength = 10;
    private GameObject focalPoint;
    private bool hasPowerUp = false;
    private float powerUpTime = 7;
    private float projectileTime = 2;
    public GameObject powerUpIndicator;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);

            if (other.gameObject.name == "PowerUp(Clone)")
            {
                hasPowerUp = true;
                StartCoroutine(PowerupCountdownRoutine());
                Debug.Log(other.gameObject.name);
            }
            else if (other.gameObject.name == "ProjectilePowerUp(Clone)")
            {
                StartCoroutine(ProjectileCountdownRoutine());
                Debug.Log(other.gameObject.name);
            }
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    IEnumerator ProjectileCountdownRoutine()
    {
        for (int i = 0; i < powerUpTime / projectileTime; i++)
        {
            CreateProjectileWave();
            yield return new WaitForSeconds(projectileTime);
        }
        powerUpIndicator.SetActive(false);
    }

    void CreateProjectileWave()
    {
        for (int i = 0; i < FindObjectsOfType<Enemy>().Length; i++)
        {
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("collided with " + collision.gameObject.name + " with poserup set to " + hasPowerUp);
        }
    }
}
