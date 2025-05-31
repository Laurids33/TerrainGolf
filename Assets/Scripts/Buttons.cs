using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Terrain1Button_Click()
    {
        SceneManager.LoadScene("Terrain1");
    }

    public void Terrain2Button_Click()
    {
        SceneManager.LoadScene("Terrain2");
    }
}
