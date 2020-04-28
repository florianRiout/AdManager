using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("Switch").GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene()
    {
        if (text.text.Equals("UnityAds"))
        {
            SceneManager.LoadScene("UnityAds");
        }
        else if (text.text.Equals("AdMob"))
        {
            SceneManager.LoadScene("AdMob");
        }
    }
}
