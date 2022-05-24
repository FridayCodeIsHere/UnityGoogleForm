using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private InputField _nameField;
    [SerializeField] private InputField _emailField;
    [SerializeField] private InputField _phoneField;
    [SerializeField] private Dropdown _phoneCodeField;

    private const int VALIDATE_PHONE = 9;

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

        using (UnityWebRequest request = UnityWebRequest.Post(BASE_URL, form))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
        }
    }

    public void SendData()
    {
        if (ValidateData())
        {
            int indexCode = _phoneCodeField.value;
            _name = _nameField.text;
            _email = _emailField.text;
            _phone = _phoneCodeField.options[indexCode].text + _phoneField.text;

            StartCoroutine(Post());
        }
    }

    private bool ValidateData()
    {
        if (_phoneField.text.Length < VALIDATE_PHONE)
        {
            Debug.LogWarning($"You have incorrect phone number. You must have 9 numbers instead {_phoneField.text.Length} after code country operator");
            return false;
        }
        if (_nameField.text.Length < 2)
        {
            Debug.LogWarning("The name is so short");
            return false;
        }
        if (!_emailField.text.Contains("@"))
        {
            Debug.LogWarning("Incorrect email, you need write @");
            return false;
        }
        return true;
    }
}
