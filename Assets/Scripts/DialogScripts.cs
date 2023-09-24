using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogEdicationMan : MonoBehaviour
{

    public string[] lines;
    public float speedText;
    public Text dialogText;

    public int index;


    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = string.Empty;
        StartDialog();
    }

    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() 
    {
        foreach(char c in lines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    public void skipText()
    {
        if (dialogText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[index];
        }
    }

    private void NextLine() 
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }
}
