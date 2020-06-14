using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut_Menu : MonoBehaviour
{
    public Image fond;


    private void OnEnable()
    {
        StartCoroutine(FadeTo(0, 2f));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        Debug.Log("debut fade");
        float alpha = fond.color.a;
        for (float t = 0.0f; t < 2.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            fond.color = newColor;

            yield return null;
        }


    }
}
