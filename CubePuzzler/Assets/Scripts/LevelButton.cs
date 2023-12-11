using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public bool unlockedStatus = false;
    public int trophy = 0;
    public Sprite lockedSprite;
    public List<Sprite> trophySprites = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        if (!unlockedStatus)
            transform.GetComponent<Image>().sprite = lockedSprite;
        else
        {
            transform.GetComponent<Image>().sprite = trophySprites[trophy];
            transform.GetComponent<Button>().interactable= true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
