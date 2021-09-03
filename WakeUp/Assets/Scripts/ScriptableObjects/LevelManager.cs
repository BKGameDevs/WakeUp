using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelManager : ScriptableObject
{
    public UnityEvent ScenedLoaded;

    public void LoadScene(string scene)
    {
        CrossFadeController.Instance.RunCrossFade(() => SceneManager.LoadScene(scene));
    }
}