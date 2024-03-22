using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
