
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float speed = 10.0f;
    public float fallLimit = -5.0f;  

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);

        if (transform.position.y < fallLimit)
        {
            GameManager.instance.GameOver();  
            Destroy(gameObject);  

        }
    }
}
