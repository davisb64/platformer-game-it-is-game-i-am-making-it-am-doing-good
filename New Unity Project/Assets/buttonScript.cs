using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    private bool die = false;
    [SerializeField] private Text buttonText;
    float timeDone = 0f;
    int currentScene;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "";
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        die = (playerMove.die);
        if (die)
        {
            buttonText.text = "idiot ded";
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "kickable")
        {
            buttonText.text = "you did it, i guess";
            if (currentScene + 1 < SceneManager.sceneCountInBuildSettings)
                {
                SceneManager.LoadScene(currentScene + 1);
            }
            else
            {
                buttonText.text = "congrats, it's done now";
            }
        }
    }
}
