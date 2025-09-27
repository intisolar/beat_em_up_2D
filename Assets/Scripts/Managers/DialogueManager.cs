using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _dialogueText;
    private string[] _dialogueArray;



    [Header("Typing")]
    private float _characterDelay = 0.025f;


    [Header("Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    private int _currentIndex = 0;
    private bool _isTyping = false;
    private Coroutine _typingRoutine;

    [Header("Dialogue Data")]
    [TextArea]
    [SerializeField] private string FileName;
    [SerializeField]
    private string[][] _dialogueData = new string[][]
{
    new string[] { "Agus", "Ey amigo… ¿no sabés dónde venden birras?" },
    new string[] { "Player", "Tranqui… acá tengo una." },
    new string[] { "Agus", "¡Ey, qué te pasa, alcahuete! Sos boleta." },
    new string[] { "Player", "Ahora a vos también te duele la cabeza…" }
};
    void Awake()
    {
        if (_dialogueText == null)
        {
            _dialogueText = GetComponentInChildren<TextMeshProUGUI>(true);

            _dialogueText.text = "";
        }
    }
    private void Start()
    {
        if (_dialogueData != null && _dialogueData.Length > 0)
        {
            OnDialogueStart?.Invoke();
            DisplayNextLine();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            EndDialogue();
            return;
        } else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            if (_isTyping) SkipTyping();
            else DisplayNextLine();
        }

    }
    private void DisplayNextLine()
    {
        if (_dialogueData == null || _currentIndex >= _dialogueData.Length)
        {
            EndDialogue();
            return;
        }
        string speaker = _dialogueData[_currentIndex][0];
        string line = _dialogueData[_currentIndex][1];
        _currentIndex++;

        if (_typingRoutine != null) StopCoroutine(_typingRoutine);
        _typingRoutine = StartCoroutine(TypeLine($"{speaker}: {line}"));
    }
    private IEnumerator TypeLine(string _fullLine)
    {
        _isTyping = true;
        _dialogueText.text = "";


        foreach (char c in _fullLine)
        {
            _dialogueText.text += c;
            yield return new WaitForSeconds(_characterDelay);
        }
        _isTyping = false;
       _typingRoutine = null;
    }

    private void SkipTyping()
    {
        if (!_isTyping) return;
        if (_typingRoutine != null) StopCoroutine(_typingRoutine);

        string speaker = _dialogueData[_currentIndex - 1][0];
        string line = _dialogueData[_currentIndex - 1][1];
        _dialogueText.text = $"{speaker}: {line}";

        _isTyping = false;
        _typingRoutine = null;
    }

    private void EndDialogue()
    {
        if (_typingRoutine != null) StopCoroutine(_typingRoutine);
        _isTyping = false;
        _typingRoutine = null;
        _dialogueText.text = "";
        OnDialogueEnd?.Invoke();
    }
}
