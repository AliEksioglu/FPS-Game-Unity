using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    static public float score;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject Pistol;
    [SerializeField] Text scoreText;
    static public bool playerIsDead { set { 
            _playerIsDead = value;
        } 
        get { return _playerIsDead; } 
    }

    static private bool _playerIsDead;
    private bool checkFinish = true;
    void Start()
    {
        score = 0f;
        Time.timeScale = 1f;
        GameOverPanel.SetActive(false);
        playerIsDead = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        if(_playerIsDead && checkFinish)
        {
            GameOverFonc();
            checkFinish = false;
        }
        
    }
    private void GameOverFonc()
    {
        StartCoroutine("GameoverWait");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameOverPanel.SetActive(true);
        Pistol.SetActive(false);
        scoreText.text = score.ToString();
    }
    IEnumerator GameoverWait()
    {
        
        while(Time.timeScale > 0.1)
        {
            Time.timeScale -= 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
        Time.timeScale = 0f;


    }
    public void ReplayButton()
    {
        SceneManager.LoadScene("MainScene");
    }
 
}
