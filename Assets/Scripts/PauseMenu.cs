using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject OverMenuUI;
    public AudioMixer audioMixer;

    private float timeStart;
    public Text textBox;
    public Text healthBox;
    public Text keysBox;
    public float minutes = 0;
    private bool timeActive = true;

    // Update is called once per frame
    void Update()
    {


        if (timeActive)
          timeStart += Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else Pause();


            
        }


        healthBox.text = "HEALTH: " + FindObjectOfType<Movement>().lives;
        keysBox.text = "KEYS: " + FindObjectOfType<Movement>().nuts;

    }

    private void FixedUpdate()
    {

        if (minutes >= 10)
        {
            if (timeStart >= 10)
                textBox.text = minutes.ToString() + ":" + (timeStart).ToString("F2");
            else
                textBox.text = minutes.ToString() + ":0" + (timeStart).ToString("F2");
        }
        else
        {

            if (timeStart >= 10)
                textBox.text = "0" + minutes.ToString() + ":" + (timeStart).ToString("F2");
            else
                textBox.text = "0" + minutes.ToString() + ":0" + (timeStart).ToString("F2");

        }


        if (timeStart >= 60f)
        {
            timeStart = 0;
            minutes++;
        }
    }

    public void EndGame()
    {
        Debug.Log("GameOver");
        OverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()

    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        timeActive = !timeActive;

    }

     void Pause()
        {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        timeActive = !timeActive;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
