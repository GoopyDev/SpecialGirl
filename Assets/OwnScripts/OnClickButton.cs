using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickButton : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private float elapsedTime;
    [SerializeField] private bool comenzarJuego;
    [SerializeField] private GameObject introMessagePanel;
    [SerializeField] private GameObject introMessage;
    [SerializeField] private GameObject transitionBG;
    [SerializeField] private float transitionBGAlpha;

    private void Update()
    {
        if (comenzarJuego)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < 0.9f)
            {
                introMessagePanel.GetComponent<Image>().color = new Color(0, 0, 0, elapsedTime);
            }
            if (elapsedTime <= 1f)
            {
                introMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255, elapsedTime);
            }
            if (elapsedTime >= transitionTime)
            {
                //introMessagePanel.SetActive(false);
                //introMessage.SetActive(false);
                transitionBGAlpha += Time.deltaTime;
                transitionBG.GetComponent<Image>().color = new Color(0, 0, 0, transitionBGAlpha);
                if (transitionBGAlpha >= 1)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialGirl01");
                }
            }

            if (Input.GetAxis("Fire1") !=0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (introMessage.activeInHierarchy == true)
                {
                    elapsedTime = transitionTime;
                }
            }
        }
    }
    public void StartGame()
    {
        comenzarJuego = true;
    }

    private void OnMouseDown()
    {
        if (comenzarJuego) { elapsedTime = transitionTime; }
    }
}
