using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public GameObject NextButton;
    public GameObject fastButton;
    public GameObject standButton;
    public GameObject crouchButton;
    public GameObject jumpButton;
    public GameObject saveButton;
    public GameObject dateText;

    public GameObject restartButton;
    public GameObject returnButton;
    public GameObject exitButton;

    public GameObject s1Button;
    public GameObject s2Button;
    public GameObject s3Button;
    public GameObject s4Button;
    public GameObject s5Button;
    public GameObject s6Button;
    public GameObject s7Button;

    // ボタンが押された場合、今回呼び出される関数
    public void NextOnClick()
    {
        SceneManager.LoadScene("Calib");
    }
    
    public void FastOnClick()
    {
        fastButton.SetActive(false);
        dateText.SetActive(true);
        standButton.SetActive(true);
    }

    public void StandOnClick()
    {
        standButton.SetActive(false);
        saveButton.SetActive(true);
    }

    public void SaveOnClick()
    {
        SceneManager.LoadScene("StartMenu");
        //SceneManager.LoadScene("Jump");
    }

    public void s1OnClick()
    {
        SceneManager.LoadScene("Stage01");
    }

    public void s2OnClick()
    {
        SceneManager.LoadScene("Stage02");
    }

    public void s3OnClick()
    {
        SceneManager.LoadScene("Stage03");
    }

    public void s4OnClick()
    {
        SceneManager.LoadScene("Stage04");
    }

    public void s5OnClick()
    {
        SceneManager.LoadScene("Stage05");
    }

    public void s6OnClick()
    {
        SceneManager.LoadScene("Stage06");
    }

    public void s7OnClick()
    {
        SceneManager.LoadScene("highScore");
    }

    public void RestartButtonOnClick()
    {
        if (SceneManager.GetActiveScene().name == "Stgae01")
        {
            SceneManager.LoadScene("Stage01");
        }
        if (SceneManager.GetActiveScene().name == "Stgae02")
        {
            SceneManager.LoadScene("Stage02");
        }
        if (SceneManager.GetActiveScene().name == "Stgae03")
        {
            SceneManager.LoadScene("Stage03");
        }
        if (SceneManager.GetActiveScene().name == "Stgae04")
        {
            SceneManager.LoadScene("Stage04");
        }
        if (SceneManager.GetActiveScene().name == "Stgae05")
        {
            SceneManager.LoadScene("Stage05");
        }
        if (SceneManager.GetActiveScene().name == "Stgae06")
        {
            SceneManager.LoadScene("Stage06");
        }
    }

    public void ReturnButtonOnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitOnClick()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }
}
