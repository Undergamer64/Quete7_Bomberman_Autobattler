using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_images;

    [SerializeField]
    private List<GameObject> m_buttons;

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Test");
    }
}
