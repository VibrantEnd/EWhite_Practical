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

    private bool isBoss;
    

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
    }

    // Update is called once per frame
    void Update()
    {
        
        navAgent.SetDestination(playerTransform.position);

    }
    public void Massacre()
    {
        if (!isBoss)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((CompareTag("Player")) && !isBoss)
        {
            Destroy(gameObject);
            player.GetComponent<Player>().TakeDamage(Damage);
        }
        else if ((CompareTag("Player")) && isBoss)
        {
            player.GetComponent<Player>().TakeDamage(Damage);
        }
        if (CompareTag("Projectile"))
        {
            currentHealth--;
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
    }
}
