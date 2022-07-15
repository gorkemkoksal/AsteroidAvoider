using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] AsteroidSpawner asteroidSpawner;
    [SerializeField] GameObject gameOverDisplay;
    public void EndGame()
    {
        asteroidSpawner.enabled = false;

        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Score: {finalScore}";
        
        gameOverDisplay.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Contunie()
    {
        scoreSystem.StartTimer();

        player.SetActive(true);
        player.transform.position = Vector3.zero;   
      //  player.transform.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        asteroidSpawner.enabled = true;
        gameOverDisplay.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
