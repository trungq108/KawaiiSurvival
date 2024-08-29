using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private TextMeshProUGUI commandText;
    [SerializeField] private string pauseCommand;
    [SerializeField] private string confirmCommand;

    [SerializeField] private Image pauseIcon;
    [SerializeField] private Sprite coffee;
    [SerializeField] private Sprite question;

    [Header("Logic")]
    [SerializeField] private GameObject gameStateButton_Section;
    [SerializeField] private GameObject confirmButton_Section;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;

    [SerializeField] private Button yupButton;
    [SerializeField] private Button nopeButton;

    private void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        resumeButton.onClick.AddListener(() => ResumeGame());
        menuButton.onClick.AddListener(() => ConfirmPopUp());
        yupButton.onClick.AddListener(() => LoadGame());
        nopeButton.onClick.AddListener(() => UnconfirmPopUp());

        UnconfirmPopUp();
    }
    private void UnconfirmPopUp()
    {
        pauseIcon.sprite = coffee;
        commandText.text = pauseCommand;
        gameStateButton_Section.SetActive(true);
        confirmButton_Section.SetActive(false);
    }

    private void ConfirmPopUp()
    {
        pauseIcon.sprite = question;
        commandText.text = confirmCommand;
        gameStateButton_Section.SetActive(false);
        confirmButton_Section.SetActive(true);
    }

    private void ResumeGame() => GameManager.Instance.ResumeGame();
    private void LoadGame() => GameManager.Instance.LoadFromPause();

}
