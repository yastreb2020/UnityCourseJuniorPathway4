using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (FindObjectOfType<Enemy>().transform.position - transform.position).normalized;
        transform.Translate(lookDirection/*new Vector3(1, 1, 0)*/ * speed * Time.deltaTime);
    }
}
