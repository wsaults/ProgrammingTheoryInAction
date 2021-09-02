using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour // INHERITANCE
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winnerText;
    public Button restartButton;
    public Button exitButton;

    public GameObject Player1GameObject;
    public GameObject Player2GameObject;

    // Board Squares
    public GameObject TLSquare;
    public GameObject TMSquare;
    public GameObject TRSquare;

    public GameObject MLSquare;
    public GameObject MMSquare;
    public GameObject MRSquare;

    public GameObject BLSquare;
    public GameObject BMSquare;
    public GameObject BRSquare;

    [SerializeField] bool isGameActive;
    [SerializeField] GameObject ActivePlayer;
    [SerializeField] Dictionary<string, string> BoardPositions = new Dictionary<string, string>();

    private int BoardSquareCount = 9;
    private string WinnerName;

    // Start is called before the first frame update
    void Start()
    {
        SetupGame();   
    }

    private void SetupGame()
    {
        WinnerName = "It's a Tie";
        isGameActive = true;
        ActivePlayer = Player1GameObject;
        BoardPositions = new Dictionary<string, string>();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        winnerText.text = $"The Winner is: {WinnerName}!";
        winnerText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    // ABSTRACTION
    public void TappedGameSquare(GameObject sqaure)
    {
        if (!isGameActive) { return; }

        if (!BoardPositions.ContainsKey(sqaure.name))
        {   
            Instantiate(ActivePlayer, sqaure.transform.position, ActivePlayer.transform.rotation);
            BoardPositions.Add(sqaure.name, ActivePlayer.gameObject.name);

            if (CheckForWin())
            {
                GameOver();
                return;
            }

            if (CheckForGameOver())
            {
                GameOver();
                return;
            }

            SetNextPlayer();
        }
    }

    private void SetNextPlayer()
    {
        ActivePlayer = ActivePlayer == Player1GameObject ? Player2GameObject : Player1GameObject;
    }

    private bool CheckForGameOver()
    {
        return BoardPositions.Count == BoardSquareCount;
    }

    private bool CheckForWin()
    {
        string owner = OwnerForWin();
        if (owner.Equals(""))
        {
            // No Winner
            return false;
        }

        WinnerName = PlayerNameForOwner(owner);
        return true;
    }

    private string PlayerNameForOwner(string owner)
    {
        if (DataManager.Instance != null)
        {
            return owner == "Player1" ? DataManager.Instance.Player1Name : DataManager.Instance.Player2Name;
        }
        return owner;
    }

    private string OwnerForWin()
    {
        string square = "Square";

        string tlSquare = "TL " + square;
        string tmSquare = "TM " + square;
        string trSquare = "TR " + square;

        string mlSquare = "ML " + square;
        string mmSquare = "MM " + square;
        string mrSquare = "MR " + square;

        string blSquare = "BL " + square;
        string bmSquare = "BM " + square;
        string brSquare = "BR " + square;

        // Rows

        if (CheckGroupComplete(tlSquare, tmSquare, trSquare))
        {
            Debug.Log("ROW 1 COMPLETE");
            return OwnerForSquare(tlSquare);
        }

        if (CheckGroupComplete(mlSquare, mmSquare, mrSquare))
        {
            Debug.Log("ROW 2 COMPLETE");
            return OwnerForSquare(mlSquare);
        }

        if (CheckGroupComplete(blSquare, bmSquare, brSquare))
        {
            Debug.Log("ROW 3 COMPLETE");
            return OwnerForSquare(blSquare);
        }

        // Columns

        if (CheckGroupComplete(tlSquare, mlSquare, blSquare))
        {
            Debug.Log("COLUMN 1 COMPLETE");
            return OwnerForSquare(tlSquare);
        }

        if (CheckGroupComplete(tmSquare, mmSquare, bmSquare))
        {
            Debug.Log("COLUMN 2 COMPLETE");
            return OwnerForSquare(tmSquare);
        }

        if (CheckGroupComplete(trSquare, mrSquare, brSquare))
        {
            Debug.Log("COLUMN 3 COMPLETE");
            return OwnerForSquare(trSquare);
        }

        // Diagonals

        if (CheckGroupComplete(tlSquare, mmSquare, brSquare))
        {
            Debug.Log("DIAGONAL 1 COMPLETE");
            return OwnerForSquare(tlSquare);
        }

        if (CheckGroupComplete(trSquare, mmSquare, blSquare))
        {
            Debug.Log("DIAGONAL 2 COMPLETE");
            return OwnerForSquare(trSquare);
        }

        return "";
    }

    private bool CheckGroupComplete(string sqaure1, string square2, string square3)
    {
        return CheckGroupFull(sqaure1, square2, square3) && CheckSquaresOwner(sqaure1, square2, square3);
    }

    private bool CheckGroupFull(string sqaure1, string square2, string square3)
    {
        return BoardPositions.ContainsKey(sqaure1) && BoardPositions.ContainsKey(square2) && BoardPositions.ContainsKey(square3);
    }

    private bool CheckSquaresOwner(string sqaure1, string square2, string square3)
    {
        return OwnerForSquare(sqaure1).Equals(OwnerForSquare(square2)) && OwnerForSquare(square2).Equals(OwnerForSquare(square3));
    }

    private string OwnerForSquare(string square)
    {
        return BoardPositions[square];
    }
}
