using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using TMPro;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navAgent;

    private GameObject player;
    private Transform playerTransform;
    private SphereCollider sphereCollider;       //Unsure if these are necessary.
    private CapsuleCollider capsuleCollider;     //Unsure if these are necessary.

    public bool isBoss;                         // This boolean tells if the enemy is the boss or not.
    

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
        currentHealth = Health;
        sphereCollider = GetComponent<SphereCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        
        navAgent.SetDestination(playerTransform.position);
        if(player.GetComponent<Player>().NewScore > 300)
        {
            Massacre();
        }

    }
    public void Massacre()                  //This function is called when the boss is spawned, and only kills non-boss enemies
    {
        if (!isBoss)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            player.GetComponent<Player>().ScoreAdd(Score); //If the enemy that died was the boss, the game finishes and restarts.
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
