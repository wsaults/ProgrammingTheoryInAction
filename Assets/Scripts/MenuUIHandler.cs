using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField Player1NameInputTextArea;
    [SerializeField] TMP_InputField Player2NameInputTextArea;

    private void Start()
    {
        Player1NameInputTextArea.text = DataManager.Instance.Player1Name;
        Player2NameInputTextArea.text = DataManager.Instance.Player2Name;
    }

    public void StartNew()
    {
        DataManager.Instance.SaveLastPlayers(
            Player1NameInputTextArea.text,
            Player2NameInputTextArea.text
        );
        // TODO: Load the main scene
        //SceneManager.LoadScene(1);
    }
}
