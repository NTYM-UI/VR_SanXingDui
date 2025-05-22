using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间

public class scenes : MonoBehaviour
{
    public TMP_Text text; // 使用TMP_Text代替Text
    public float timePerChar = 0.1f; // 每个字符的显示时间

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText("三星堆面具是古蜀文明的杰出代表，以其独特的造型、精湛的工艺和深厚的文化内涵，展现了古蜀人对神灵的崇拜、社会等级的象征以及独特的审美观念，是中华文明多元起源的重要见证。"));
    }

    // 逐字显示文本的协程
    private IEnumerator ShowText(string content)
    {
        text.text = ""; // 清空文本
        foreach (char c in content)
        {
            text.text += c; // 逐字添加到文本中
            yield return new WaitForSeconds(timePerChar); // 等待指定时间
        }
    }
}