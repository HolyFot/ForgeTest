using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarterScene : MonoBehaviour
{
    public Toggle isServer1;

    public void StartForgeGame()
    {
        //Start the Game as a Server or a Client
        if (isServer1.isOn)
        {
            Debug.Log("Starting Server!");
            GameSettings.isServer = true;
        }
        else
        {
            Debug.Log("Starting Client!");
            GameSettings.isServer = false;
        }

        SceneManager.LoadScene("CharSelect");
    }
}