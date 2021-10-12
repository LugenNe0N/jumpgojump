using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public GameObject score_object01 = null; // Textオブジェクト
    public int score_s1 = 0; // スコア変数
    public GameObject score_object02 = null;
    public int score_s2 = 0;
    public GameObject score_object03 = null;
    public int score_s3 = 0;
    public GameObject score_object04 = null;
    public int score_s4 = 0;
    public GameObject score_object05 = null;
    public int score_s5 = 0;
    public GameObject score_object06 = null;
    public int score_s6 = 0;


    // 初期化時の処理
    void Start()
    {
        // スコアのロード
        score_s1 = PlayerPrefs.GetInt("s1", 0);
        score_s2 = PlayerPrefs.GetInt("s2", 0);
        score_s3 = PlayerPrefs.GetInt("s3", 0);
        score_s4 = PlayerPrefs.GetInt("s4", 0);
        score_s5 = PlayerPrefs.GetInt("s5", 0);
        score_s6 = PlayerPrefs.GetInt("s6", 0);
    }
    // 削除時の処理
    void OnDestroy()
    {
        // スコアを保存
        PlayerPrefs.SetInt("s1", score_s1);
        PlayerPrefs.SetInt("s2", score_s2);
        PlayerPrefs.SetInt("s3", score_s3);
        PlayerPrefs.SetInt("s4", score_s4);
        PlayerPrefs.SetInt("s5", score_s5);
        PlayerPrefs.SetInt("s6", score_s6);
        PlayerPrefs.Save();
    }

    // 更新
    void Update()
    {
        Text score01_text = score_object01.GetComponent<Text>(); // オブジェクトからTextコンポーネントを取得
        score01_text.text = "Score:" + score_s1; // テキストの表示を入れ替える

        Text score02_text = score_object02.GetComponent<Text>();
        score02_text.text = "Score:" + score_s2;

        Text score03_text = score_object03.GetComponent<Text>();
        score03_text.text = "Score:" + score_s3;

        Text score04_text = score_object04.GetComponent<Text>();
        score04_text.text = "Score:" + score_s4;

        Text score05_text = score_object05.GetComponent<Text>();
        score05_text.text = "Score:" + score_s5;

        Text score06_text = score_object06.GetComponent<Text>();
        score06_text.text = "Score:" + score_s6;
    }
}
