using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingForward : MonoBehaviour
{
    public float speed;
    private GameManager gameManagerScript;
    public Vector3 startPos;
    private Collider2D col;
    
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        col = GetComponent<Collider2D>();
    }


    void Update()
    {
        if (gameManagerScript.isGameActive)
        {
            Moving();
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }

    void Moving()
    {
        if (CompareTag("Background") && transform.position.x < (startPos.x - 19f))  //Імітація безкінечного заднього фону
        {
            transform.position = startPos;
        }

        transform.position += (Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -11 && !CompareTag("Background"))  //Видалення труби при досяганні межі
        {
            Destroy(gameObject);
        }
    }
}
