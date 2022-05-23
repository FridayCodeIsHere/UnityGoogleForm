using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private InputField _nameField;
    [SerializeField] private InputField _emailField;
    [SerializeField] private InputField _phoneField;

    private string _name;
    private string _email;
    private string _phone;

    private const string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScnhEMZtdy4ewFI1pBYnt6Rl_glCm7O7iaZv7eluEYK4XdO_Q/formResponse";

    private IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.604633462", _name);
        form.AddField("entry.723998528", _email);
        form.AddField("entry.2076706937", _phone);

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }

    public void SendData()
    {
        _name = _nameField.text;
        _email = _emailField.text;
        _phone = _phoneField.text;

        StartCoroutine(Post());
    }
}
