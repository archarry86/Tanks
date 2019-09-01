using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Text text;

    public string nextScene;

    private bool started = false;

    private float time = 0f;
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (time == 0)
        {
            time = Time.time + 1.2f;
            return;
        }

        if (Time.time < time)
        {
            return;
        }

        if (!started)
        {
            started = true;
            StartCoroutine(LoadScene());
        }

    }

    IEnumerator LoadScene()
    {
        var op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene);

        while (!op.isDone)
        {
            var progress = op.progress;

            text.text = "CARGANDO " + Mathf.Round(progress * 100f);

            yield return null;
        }
        text.text = "CARGANDO 100";
        yield return null;
    }
}
