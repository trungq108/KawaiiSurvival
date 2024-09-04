using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    [SerializeField] private Button sfxButton;
    private Image sfxImage;
    private TextMeshProUGUI sfxText;
    private bool sfxState;

    [SerializeField] private Button audioButton;
    private Image audioImage;
    private TextMeshProUGUI audioText;
    private bool audioState;

    [SerializeField] private Button policyButton;
    [SerializeField] private Button askButton;
    [SerializeField] private Button showCredit;
    [SerializeField] private Button hideCredit;
    [SerializeField] private GameObject creditTab;

    private const string ON = "ON";
    private const string OFF = "Off";

    private void Awake()
    {
        sfxButton.onClick.AddListener(() => ChangeSFX());
        sfxImage = sfxButton.GetComponent<Image>();
        sfxText = sfxButton.GetComponentInChildren<TextMeshProUGUI>();
        sfxState = ES3.Load<bool>("sfxState", true);
        UpdateSFX();

        audioButton.onClick.AddListener(() => ChangeAudio());
        audioImage = audioButton.GetComponent<Image>();
        audioText = audioButton.GetComponentInChildren<TextMeshProUGUI>();
        audioState = ES3.Load<bool>("audioState", true);
        UpdateAudio();

        policyButton.onClick.AddListener(() => OpenPolicy());
        askButton.onClick.AddListener(() => SendHelpToEmail());
        showCredit.onClick.AddListener(() => UpdateCreditTab(true));
        hideCredit.onClick.AddListener(() => UpdateCreditTab(false));
        UpdateCreditTab(false);
    }

    private void ChangeSFX()
    {
        sfxState = !sfxState;
        UpdateSFX();
    }
    private void UpdateSFX()
    {
        if (sfxState)
        {
            sfxImage.color = onColor;
            sfxText.text = ON;
            ES3.Save<bool>("sfxState", sfxState);
            GameEvent.OnSFXChange?.Invoke(sfxState);
        }
        else
        {
            sfxImage.color = offColor;
            sfxText.text = OFF;
            ES3.Save<bool>("sfxState", sfxState);
            GameEvent.OnSFXChange?.Invoke(sfxState);
        }
    }

    private void ChangeAudio()
    {
        audioState = !audioState;
        UpdateAudio();
    }
    private void UpdateAudio()
    {
        if (audioState)
        {
            audioImage.color = onColor;
            audioText.text = ON;
            ES3.Save<bool>("audioState", audioState);
            GameEvent.OnMusicChange?.Invoke(audioState);
        }
        else
        {
            audioImage.color = offColor;
            audioText.text = OFF;
            ES3.Save<bool>("audioState", audioState);
            GameEvent.OnMusicChange?.Invoke(audioState);
        }
    }

    private void OpenPolicy() => Application.OpenURL("https://www.youtube.com/watch?v=OfqrTMm_q1s");
    private void SendHelpToEmail()
    {
        string email = "trungq108@gmail.com";
        string subject = MyEscapeURL("Help!");
        string body = MyEscapeURL("Hey, I need help in this ...");

        Application.OpenURL("mailto:" +  email + "?subject=" +  subject + "&body" +  body);
    }

    private string MyEscapeURL(string s)
    {
        return UnityWebRequest.EscapeURL(s).Replace("+", "%20");
    }

    public void UpdateCreditTab(bool boolen)
    {
        creditTab.SetActive(boolen);
    }
}
