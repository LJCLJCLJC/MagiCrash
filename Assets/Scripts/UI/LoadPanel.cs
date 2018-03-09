using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadPanel : MonoBehaviour
{
    public Image bar;
    private AsyncOperation op;


    void Start () {
        StartCoroutine(LoadScene());
	}

    private void Update()
    {
        if (op.progress < 0.9f)
            bar.fillAmount = op.progress;
        else
            bar.fillAmount = 1;
    }
    IEnumerator LoadScene()
    {
        op = SceneManager.LoadSceneAsync(GameRoot.Instance.currentLoadScene);
        yield return op;
        System.GC.Collect();
        GameRoot.Instance.evt.Clear();
        DataManager.Instance.GetAllPlayer();

    }
}
