using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifeTime = 5.0f;
    SphereCollider sphereCollider;
    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine(LifeTime());
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;
        Enemy enemyScript = otherGameObject.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage();
        }
    }

    void Update()
    {
        
    }
}
