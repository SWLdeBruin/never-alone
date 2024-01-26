using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsCollectedDialog : MonoBehaviour
{
    [SerializeField] private List<string> _dialogLines = new List<string>();

    [Space(10)]

    [SerializeField] private float _textWriterSpeed = 0.05f;
    [SerializeField] private float _textWriterPause = 2f;

    [Space(10)]

    [SerializeField] private AudioSource _typeWriterSource;

    private TMP_Text _dialogText;

    private int _currentTextIndex = 0;

    private void Start()
    {
        _dialogText = GetComponent<TMP_Text>();
    }

    public void StartInventoryFinishedDialog()
    {
        StartCoroutine(TextWriter());
    }

    private IEnumerator TextWriter()
    {
        string text = _dialogLines[_currentTextIndex];

        _dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            _dialogText.text += letter;
            if (_typeWriterSource != null)
            {
                _typeWriterSource.Play();
            }
            yield return new WaitForSeconds(_textWriterSpeed);
        }

        _currentTextIndex++;
        if (_currentTextIndex < _dialogLines.Count)
        {
            yield return new WaitForSeconds(_textWriterPause);
            StartCoroutine(TextWriter());
        }
    }
}
