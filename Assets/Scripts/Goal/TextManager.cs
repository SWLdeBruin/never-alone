using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    [SerializeField] private List<string> _textList;

    [SerializeField] private float _textWriterSpeed = 0.05f;
    [SerializeField] private float _textWriterPause = 2f;
    [SerializeField] private float _textWriterPauseLast = 5f;

    [Space(10)]

    [SerializeField] private int _nextLevelId = 2;

    [Space(10)]

    [SerializeField] private AudioSource _typeWriterSource;

    private int _currentTextIndex = 0;

    private IEnumerator TextWriter()
    {
        string text = _textList[_currentTextIndex];

        _textField.text = "";
        foreach (char letter in text.ToCharArray())
        {
            _textField.text += letter;
            if (_typeWriterSource != null)
            {
                _typeWriterSource.Play();
            }
            yield return new WaitForSeconds(_textWriterSpeed);
        }

        _currentTextIndex++;
        if (_currentTextIndex >= _textList.Count)
        {
            yield return new WaitForSeconds(_textWriterPauseLast);
            UnityEngine.SceneManagement.SceneManager.LoadScene(_nextLevelId);
        }
        else
        {
            yield return new WaitForSeconds(_textWriterPause);
            StartCoroutine(TextWriter());
        }
    }

    private void Start()
    {
        StartCoroutine(TextWriter());
    }
}
