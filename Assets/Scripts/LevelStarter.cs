using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarter : MonoBehaviour
{
    public string n;
    public void Starter()
    {
        SceneManager.LoadScene(n);
    }
    public void Active(GameObject a)
    {
        a.SetActive(!a.activeSelf);
    }
}
