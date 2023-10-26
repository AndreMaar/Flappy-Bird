using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject tube;
    public bool isGameActive = false, strartedOnes = false;
    public GameObject bird;
    public GameObject loseScreen;
    private BirdController birdScript;
    public Text scoreText, highScoreText;
    public int score, highScore;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            highScore = PlayerPrefs.GetInt("SavedScore");
            highScoreText.text = "" + highScore;
        }
    }
    
    void Start()
    {
        birdScript = bird.GetComponent<BirdController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive == false && strartedOnes == false)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        strartedOnes = true;
        InvokeRepeating("Spawn", 3, 2);
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(false);
        isGameActive = true;
        birdScript.isGameActive = true;
        birdScript.StartGame();
    }
    public void ScorePlus()
    {
        score += 1;
        scoreText.text = "" + score;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("SavedScore", highScore);
            highScoreText.text = "" + highScore;
        }
        Debug.Log(score);
    }
    

    void Spawn()
    {
        Instantiate(tube, new Vector2(14, Random.Range(-3.4f, 1.9f)), tube.transform.rotation);
    }

    public void GameOver()
    {
        isGameActive = false;
        loseScreen.gameObject.SetActive(true);
        CancelInvoke();
    }

    public void LoadScene(int sceneNumber)  //Перехід на іншу сцену
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void RestartScene()  //Перезапуск сцени
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
