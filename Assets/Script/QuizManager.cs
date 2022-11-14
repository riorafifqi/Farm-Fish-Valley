using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public SoalQuiz soalQuiz;
    public TMP_Text tombolLU, tombolLD, tombolRU, tombolRD;
    public TMP_Text soal;
    public EndGame endGame;
    public bool result;

    private void Awake()
    {
        endGame = GameObject.Find("GameManager").GetComponent<EndGame>();
    }

    private void Start()
    {
        Time.timeScale = 0;     // pause game when quiz in progress
        soal.text = soalQuiz.soal;          // set soal and answer to UI
        tombolLU.text = soalQuiz.tombolLU;
        tombolLD.text = soalQuiz.tombolLD;
        tombolRU.text = soalQuiz.tombolRU;
        tombolRD.text = soalQuiz.tombolRD;
    }

    public void PressAnswer()
    {
        Debug.Log("Selected answer: " + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        Debug.Log("Right: " + soalQuiz.jawabanBenar);
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == soalQuiz.jawabanBenar)       // if the pressed button name is the same as the correct answer
        {
            // Jawaban benar
            Debug.Log("Benar");
            Time.timeScale = 1;     // unpause game

            result = true;      // trigger quiz to close
            //transform.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Salah");
            GetComponent<SettingPanelAnimation>().Close();
            //jawaban salah

            endGame.Lose();     // lose game
            result = false;
        }
    }
}
