using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    // we can drop this class in any UI scene to load the proper logic scene
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private ConcurrentScenesCollection m_ScenesCollection;

        private void Awake()
        {
            LoadScenes();   
        }

        private void OnDestroy()
        {
            UnloadScenes();   
        }

        private void LoadScenes()
        {
            IEnumerable<string> scenesNames = m_ScenesCollection.GetScenesNames();

            foreach (string sceneName in scenesNames)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        // it's not the most important thing of this project so let's drop this warning
#pragma warning disable 0618
        private void UnloadScenes()
        {
            IEnumerable<string> scenesNames = m_ScenesCollection.GetScenesNames();

            foreach (string sceneName in scenesNames)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadScene(sceneName);
            }
        }
#pragma warning restore 0618
    }
}
