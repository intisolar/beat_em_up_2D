using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;

    void Start()
    {
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");

        if (existingPlayer != null)
        {
            Debug.LogWarning("Ya existe un GameObject con tag 'Player'. No se instanciará otro.");
            transform.SetParent(existingPlayer.transform);
            return;
        }

        int _indexPlayer = PlayerPrefs.GetInt("PlayerIndex");
        GameObject newParent;

        if (_characterData != null && _indexPlayer < _characterData.Characters.Count && _indexPlayer >= 0)
        {
            newParent = Instantiate(_characterData.Characters[_indexPlayer].Player);
        }
        else
        {
            newParent = Instantiate(_characterData.Characters[0].Player);

            if (_characterData == null)
            {
                Debug.LogWarning("Lista de CharacterData no está asignada. Usando prefab por defecto para el jugador.");
            }
            else if (_indexPlayer < 0 || _indexPlayer >= _characterData.Characters.Count)
            {
                Debug.LogWarning("Índice fuera de rango. Usando prefab por defecto para el jugador.");
            }
            else
            {
                Debug.LogWarning("Error. Usando prefab por defecto para el jugador.");
            }
        }

        newParent.transform.position = transform.position;
        newParent.transform.SetParent(transform);
    }
}
