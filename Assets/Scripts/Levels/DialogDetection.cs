using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class DialogTagWithText
{
    public string Tag = "";
    public string Text = "";

    public DialogTagWithText(string tag, string text)
    {
        Tag = tag;
        Text = text;
    }
}

public class DialogDetection : MonoBehaviour
{
    [SerializeField] private List<DialogTagWithText> _dialogObjectTags;

    [SerializeField] private TMP_Text _dialogText;

    [SerializeField] private float _textWriterSpeed = 0.05f;
    [SerializeField] private float _textWriterPause = 2f;

    private List<string> _ranDialogs = new();

    private Camera _playerCamera;
    private Plane[] _cameraFrustum;

    private void Awake()
    {
        _playerCamera = GetComponent<Camera>();
    }

    private void Update() 
    {
        _cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_playerCamera);
        foreach (DialogTagWithText dialogTagWithText in _dialogObjectTags)
        {
            string tag = dialogTagWithText.Tag;
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                if (GeometryUtility.TestPlanesAABB(_cameraFrustum, obj.GetComponent<Collider2D>().bounds))
                {
                    if (!_ranDialogs.Contains(dialogTagWithText.Tag))
                    {
                        StartCoroutine(TextWriter(dialogTagWithText.Text));
                        _ranDialogs.Add(dialogTagWithText.Tag);
                    }
                }
            }
        }
    }

    private IEnumerator TextWriter(string text)
    {
        _dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(_textWriterSpeed);
        }

        yield return new WaitForSeconds(_textWriterPause);
        _dialogText.text = "";
    }
}
