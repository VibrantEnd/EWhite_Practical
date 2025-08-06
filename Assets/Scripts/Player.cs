using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;

    private float rotationY;
    public float Sensitivity = 1.0f;

    public static Transform PlayerTransform;
    public static GameObject MainPlayer;

    public float MoveSpeed = 5.0f;
    public float horizontalInput;
    public float verticalInput;

    public GameObject ProjectilePrefab;
    public float FireCooldown = 1.0f;
    private float currentCooldown;
    public float FireVelocity = 10.0f;

    private float maxHealth = 100.0f;
    private float currentHealth;
    public TextMeshProUGUI healthText;

    public float CurrentScore;
    public float NewScore;

    public TextMeshProUGUI scoreText;

    public GameObject BossPrefab;
    public bool BossSpawned;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        PlayerTransform = GetComponent<Transform>();
        MainPlayer = GetComponent<GameObject>();
        currentCooldown = FireCooldown;
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
        CurrentScore = 0;
        scoreText.text = CurrentScore.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        currentCooldown -= Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && currentCooldown<=0)
        {
            FireProjectile();
        }
        PlayerTransform = transform;
    }
    private void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime);
    }
    private void Rotation()
    {
        Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse X") * Sensitivity;
        PlayerTransform.Rotate(Vector3.up * mouseY);

    }
    private void FireProjectile()
    {
        GameObject projectileClone = Instantiate(ProjectilePrefab, PlayerTransform.position + transform.forward * .5f, PlayerTransform.rotation);
        Rigidbody projectileRb = projectileClone.GetComponent<Rigidbody>();
        projectileRb.AddForce(PlayerTransform.forward*FireVelocity, ForceMode.Impulse);
        currentCooldown = FireCooldown;
    }
    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        Debug.Log("Your health is " + currentHealth);
        if (currentHealth < 0)
        {
            Debug.Log("You LOSE!!!!");
            Restart();
        }
        healthText.text = currentHealth.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ScoreAdd(float score)
    {
        NewScore = score + CurrentScore;
        scoreText.text = NewScore.ToString();
        CurrentScore = NewScore;
        if (NewScore >= 300 && !BossSpawned)
        {
            Instantiate(BossPrefab, new Vector3(0,2,0) + transform.position, Quaternion.identity);
            BossSpawned = true;
        }
    }
}
