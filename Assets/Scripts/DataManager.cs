using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // ENCAPSULATION
    public static DataManager Instance { get; private set; }
    public string Player1Name { get; private set; }
    public string Player2Name { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLastPlayers();
    }

    [System.Serializable]
    class SavePlayerData
    {
        public string LastPlayer1Name;
        public string LastPlayer2Name;
    }

    // ABSTRACTION
    public void SaveLastPlayers(string player1Name, string player2Name)
    {
        SavePlayerData data = new SavePlayerData();
        Player1Name = player1Name;
        Player2Name = player2Name;
        data.LastPlayer1Name = player1Name;
        data.LastPlayer2Name = player2Name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playersavefile.json", json);
    }

    // ABSTRACTION
    private void LoadLastPlayers()
    {
        string path = Application.persistentDataPath + "/playersavefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);

            Player1Name = data.LastPlayer1Name;
            Player2Name = data.LastPlayer2Name;
        }
    }
}
