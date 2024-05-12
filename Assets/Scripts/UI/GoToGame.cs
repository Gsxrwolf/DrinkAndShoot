using System.Collections;
using UnityEngine;

namespace UI
{

    public class GoToGame : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2.0f);

            SceneLoader.Instance.LoadScene(MyScenes.Game);
        }
    }
}
