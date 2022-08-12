using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public SoalQuiz soalQuiz;
    public Text tombol1, tombol2;
    public Text soal;
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
        tombol1.text = soalQuiz.tombol1;
        tombol2.text = soalQuiz.tombol2;
    }

    public void PressAnswer()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == soalQuiz.jawabanBenar)       // if the pressed button name is the same as the correct answer
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
            //jawaban salah

            endGame.Lose();     // lose game
            result = false;
        }
    }
}
