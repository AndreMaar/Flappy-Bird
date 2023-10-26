using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5;
    public float degrees;
    private float degreesPlus = -1.5f;
    public bool isGameActive = false;
    private Rigidbody2D birdRb;
    private GameManager gameManagerScript;
    private Animator anim;
    public AudioClip jumpSound;
    private AudioSource playerAudio;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        birdRb = GetComponent<Rigidbody2D>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
        birdRb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)  //Підпригування
        {
            playerAudio.PlayOneShot(jumpSound, 0.8f);
            birdRb.velocity = new Vector3(0, jumpForce, 0);
            degrees = 90;
            degreesPlus = -1.5f;
            transform.rotation = Quaternion.Euler(Vector3.forward * 25);
        }
    }

    public void StartGame()  //Початковий стрибок що запускається з GameManager
    {
        playerAudio.PlayOneShot(jumpSound, 0.8f);
        birdRb.gravityScale = 1;
        birdRb.velocity = new Vector3(0, jumpForce, 0);
        degrees = 180;
        degreesPlus = -1.5f;
        transform.rotation = Quaternion.Euler(Vector3.forward * 25);
    }

        private void FixedUpdate()
    {
        if (isGameActive)
        {
            if (degrees > -90 )  //Імітація падіння
            {
                degrees += degreesPlus;
                degreesPlus += -0.08f;
            }
            if (degrees < 25)  //Встановлення кута повороту при стрибку
            {
                transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)  //Програш при доторканні до колайдерів
    {
        if (collision.gameObject.CompareTag("Tube"))
        {
            Debug.Log("Lose");
            isGameActive = false;
            gameManagerScript.GameOver();
            anim.enabled = false;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  //Отримання балів
    {
        if (collision.CompareTag("Tube"))
        {
            gameManagerScript.ScorePlus();
        }
    }
}
