
using UnityEngine;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3.0f;  
    private Vector3 targetPosition;  
    private float timeToChangeDirection = 3.0f;  
    private float timer;
    
    private float minX = -20.41f;  
    private float maxX = 25.59f;   
    private float minZ = -10.2f;    
    private float maxZ = 30.8f;    
    void Start()
    {
        timer = timeToChangeDirection;
        SetNewTargetPosition();  

    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SetNewTargetPosition();
            timer = timeToChangeDirection;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,  
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );
    }

    void SetNewTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);  
                GameManager.instance.GameOver();  
            }
        }
    }
}
