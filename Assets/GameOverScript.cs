using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity");
    }
    public void Replay()
    {
        SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");
    }
}