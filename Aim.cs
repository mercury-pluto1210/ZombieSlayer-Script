using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 追加しましょう（ポイント）
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    public Text reticle;

    void Update()
    {
        // レーザー（ray）を飛ばす「起点」と「方向」
        Ray ray = new Ray(transform.position, transform.forward);

        // rayのあたり判定の情報を入れる箱を作る。
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            string hitName = hit.collider.gameObject.tag;

            if (hitName == "Enemy")
            {
                // 照準器の色を「赤」に変える
                reticle.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                // 照準器の色を「白色」
                reticle.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
        else
        {
            // 照準器の色を「白色」
            reticle.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}