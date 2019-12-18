using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G11_Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void NextScene()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("G11_L1_SceneName", LoadSceneMode.Single);
    }
    public void NextScene1()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("G11_L2_SceneName", LoadSceneMode.Single);
    }
    public void NextScene2()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("G11_L3_SceneName", LoadSceneMode.Single);
    }
}
