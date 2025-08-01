using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed = 10.0f;
    private float lifeTime = 5.0f;
    void Awake()
    {
        StartCoroutine(LifeTime());
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
