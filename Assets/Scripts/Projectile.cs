using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10;
    private Transform enemyToFollow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyToFollow != null)
        {
            Vector3 lookDirection = (enemyToFollow.position - transform.position).normalized;
            transform.Translate(lookDirection/*new Vector3(1, 1, 0)*/ * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // sets transform component of eney to follow and destroys projectile after the certain amount of time
    public void SetTarget(Transform target)
    {
        enemyToFollow = target;
        Destroy(gameObject, GameObject.Find("Player").GetComponent<PlayerController>().projectileTime);
    }
}
