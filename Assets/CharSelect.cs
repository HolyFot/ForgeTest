using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public Dropdown playerMenu;
    public string playerName;

    public void EnterWorld()
    {
        playerName = playerMenu.itemText.text;
        Debug.Log("Enter World, player " + playerName);
        SceneManager.LoadScene("MainScene");
    }

}