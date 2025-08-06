using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using TMPro;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navAgent;

    private GameObject player;
    private Transform playerTransform;
    private GameManager gameManager;
    private SphereCollider sphereCollider;
    private CapsuleCollider capsuleCollider;

    public bool isBoss;
    

    public float Health;
    public float Damage;
    public float Speed;
    public float Score;

    private float currentHealth;
    void Awake()
    {
        currentHealth = Health;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = Speed;
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        gameManager = GetComponent<GameManager>();
        currentHealth = Health;
        sphereCollider = GetComponent<SphereCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        navAgent.SetDestination(playerTransform.position);
        if(player.GetComponent<Player>().NewScore > 300)
        {
            Massacre();
        }

    }
    public void Massacre()
    {
        if (!isBoss)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log("Enemy health is " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            player.GetComponent<Player>().ScoreAdd(Score);
            if (isBoss)
            {
                Debug.Log("YOU WON!!!");
                player.GetComponent<Player>().Restart();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Player") && !isBoss)
        {
            Destroy(gameObject);
            player.GetComponent<Player>().TakeDamage(Damage);
            player.GetComponent<Player>().ScoreAdd(Score);
        }
        else if (collision.gameObject.CompareTag("Player") && isBoss)
        {
            player.GetComponent<Player>().TakeDamage(Damage);
        }
    }
}
