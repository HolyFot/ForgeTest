using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public Dropdown playerMenu;
    private string playerName;

    public void EnterWorld()
    {
        playerName = playerMenu.captionText.text;

        GameSettings.chosenChar = playerName; //Set Global Game Settings

        Debug.Log("Spawn Player: " + playerName);
        SceneManager.LoadScene("MainScene");
    }

}