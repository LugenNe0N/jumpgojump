using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    //ヒエラルキーからD&Dしておく
    public AudioSource BGM_title;
    public AudioSource BGM_main;
    public AudioSource BGM_Clear;
    public AudioSource BGM_Over;

    //１つ前のシーン名
    private string beforeScene = "Title";

    public bool FirstPlay = true;

    // Use this for initialization
    void Start()
    {
        //自分と各BGMオブジェクトをシーン切り替え時も破棄しないようにする
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(BGM_title.gameObject);
        DontDestroyOnLoad(BGM_main.gameObject);

        DontDestroyOnLoad(BGM_Clear.gameObject);
        DontDestroyOnLoad(BGM_Over.gameObject);

        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        FlagManager.Instance.flags[9] = false;
        FlagManager.Instance.flags[90] = false;
    }

    void Update()
    {
        if (FirstPlay == true) {
            if (FlagManager.Instance.flags[9] == true)
            {
                BGM_main.Stop();
                BGM_Clear.Play();
                FirstPlay = false;
            } else if (FlagManager.Instance.flags[90] == true)
            {
                BGM_main.Stop();
                BGM_Over.Play();
                FirstPlay = false;
            }
        }

    }

    //シーンが切り替わった時に呼ばれるメソッド
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //シーンがどう変わったかで判定

        //メニューからメインへ
        if (beforeScene == "StartMenu" && nextScene.name == "Stage01")
        {
            BGM_title.Stop();
            BGM_main.Play();
        }else if (beforeScene == "StartMenu" && nextScene.name == "Stage02")
        {
            BGM_title.Stop();
            BGM_main.Play();
        }else if (beforeScene == "StartMenu" && nextScene.name == "Stage03")
        {
            BGM_title.Stop();
            BGM_main.Play();
        }else if (beforeScene == "StartMenu" && nextScene.name == "Stage04")
        {
            BGM_title.Stop();
            BGM_main.Play();
        }else if (beforeScene == "StartMenu" && nextScene.name == "Stage05")
        {
            BGM_title.Stop();
            BGM_main.Play();
        }else if (beforeScene == "StartMenu" && nextScene.name == "Stage06")
        {
            BGM_title.Stop();
            BGM_main.Play();
        }

        //メインからメニューへ
        if ((beforeScene == "Stage01" || beforeScene == "Stage02" || beforeScene == "Stage03"
            || beforeScene == "Stage04" || beforeScene == "Stage05" || beforeScene == "Stage06") && nextScene.name == "StartMenu")
        {
            //BGM_main.Stop();
            BGM_Clear.Stop();
            BGM_Over.Stop();
            
            FlagManager.Instance.flags[9] = false;
            FlagManager.Instance.flags[90] = false;
            FirstPlay = true;
            BGM_title.Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}
