using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    [SerializeField] private float _timeSpeed = 1f;
    [SerializeField] private int _gameOverLevelId = 0;

    private DateTime _startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0);
    private DateTime _endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0);
    private DateTime _currentTime;

    private TMP_Text _timeText;

    void Start()
    {
        _currentTime = _startTime;

        _timeText = GetComponent<TMP_Text>();

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        _currentTime = _currentTime.AddSeconds(1 * _timeSpeed);

        if (_currentTime >= _endTime)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_gameOverLevelId);
        }
        else
        {
            int hours = _currentTime.Hour;
            int minutes = _currentTime.Minute;

            _timeText.text = $"{(hours < 10 ? "0" + hours : hours)}:{(minutes < 10 ? "0" + minutes : minutes)}";
        }
    }
}
