using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public Dropdown playerMenu;
    public GameClient client1;
    private string playerName;

    public void EnterWorld()
    {
        playerName = playerMenu.captionText.text;
        GameSettings.chosenChar = playerName; //Set Global Game Settings

        SceneManager.LoadScene("MainScene");

        if (GameSettings.isServer == false)
            client1.RequestSpawnPlayer(playerName);
    }

}