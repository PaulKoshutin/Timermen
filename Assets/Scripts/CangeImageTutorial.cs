using UnityEngine;
using UnityEngine.UI;

public class CangeImageTutorial : MonoBehaviour
{
    public Image image;
    public Sprite[] sprite;
    private int index = 1;

    public void NextImage()
    {
        if (index+1 > sprite.Length)
        {
            index = 0;
        }

        image.sprite = sprite[index];
        index++;
    }

    public void BackImage()
    {
        if (index == -1)
        {
            index = sprite.Length-1;
        }

        image.sprite = sprite[index];
        index--;
    }
}
