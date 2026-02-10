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
    string[][] _dialogueData = new string[][]
{
    new string[] { null, "Faisán", "Che despertate, me dijiste que ibas a ser copiloto y ni un mate." },
    new string[] { null, "Quique", "Paraaa no ves que me acosté a las 5, el cuerpo pasa factura. Y ustedes cantando una de iglesia." },
    new string[] { null, "Quique", "A ver prendé la radio, poné la Rock&Rocker ¡Silencio ahí atrás!" },
    new string[] { null, "Rock&Rocker", "Fzsshhsh … “¡El tema que no podés dejar de escuchar aunque quieras, aunque lo odies, aunque quieras prender fuego la radio…“" },
        new string[] { null, "Rock&Rocker", "‘Se Acaba’! Y suena así…" },
    new string[] { "PlayMenuSong", null, "Suena la canción de Tour Nocturno del menú del juego." },
    new string[] { null, "Quique", "Fua, ¿y esta mierda?" },
    new string[] { null, "Faisán", "No se bro, estuvo sonando toda la semana, insoportable." },
    new string[] { null, "Michi", "¿No son los que cierran el festival?" },
    new string[] { null, "Quique", "¡Sí! Los Noctámbulos ¿no?" },
    new string[] { null, "Michi", "Noctámbulo es un juego hecho por Ota Pxl ¡Está re bueno!" },
    new string[] { null, "Quique", "¡No! Tour Nocturno. Fui a la secundaria con el guitarrista. Un nabo. Se la re creía. Me robó a mi novia." },
    new string[] { null, "Faisán", "Igual fue en segundo año, teníamos 13." },
    new string[] { null, "Michi", "…El protagonista es un conejito re picante que…" },
    new string[] { null, "Quique", "¡Y todavía me duele! El amor de mi vida." },
    new string[] { null, "Faisán", "Ya lo vamos a agarrar solo a ese…" },
    new string[] { null, "Quique", "¡Apagá la radio, loco!" },
    new string[] { null, "Faisán", "Dale. Vos seguí con la misa fogonera esa." },
    new string[] { "ResumeTrip", null, "Retoma Michi la de ir de paseo en un auto feo" }};


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
        string speaker = _dialogueData[_currentIndex][1];
        string line = _dialogueData[_currentIndex][2];
        SetPortrait(speaker);
        _currentIndex++;

        if (_typingRoutine != null) StopCoroutine(_typingRoutine);
        string fullLine = speaker != null ? $"{speaker}: {line}" : line;
        _typingRoutine = StartCoroutine(TypeLine(fullLine));
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

        string speaker = _dialogueData[_currentIndex - 1][1];
        string line = _dialogueData[_currentIndex - 1][2];
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
        CinematicManager.LoadLevelOne();
    }

    private void SetPortrait(string speaker)
    {

    }

}
