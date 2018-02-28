using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneName
{
    public const string StartScene = "StartScene";
    public const string LoadScene = "LoadScene";
    public const string Level1_1 = "Level1_1";
}
public class LoadManager
{
    public static void Load(string sceneName)
    {
        GameRoot.Instance.currentLoadScene = sceneName;
        SceneManager.LoadScene(SceneName.LoadScene);
    }
}
