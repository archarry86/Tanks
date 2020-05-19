using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroGameViewController : MonoBehaviour
{

   

    public Image SelectorElement;

    public int GapBetweenOptions = 25;

    private float InitialSelectorPosition = 0;

    [SerializeField]
    private int NUMBEROFSELECTIONS = 4;

    private int[] positions =null;

    private int iterations = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitialSelectorPosition = SelectorElement.rectTransform.position.y;

        positions = new int[NUMBEROFSELECTIONS];
        positions[0] = (int) InitialSelectorPosition;

        for (int i = 1; i < positions.Length;i++)
        {
            positions[i] = positions[i - 1] - GapBetweenOptions;
        }

    }

    // Update is called once per frame
    void Update()
    {
        var wasset = false;
        if (Input.GetKeyDown(KeyCode.DownArrow) ) {
            wasset = true;
            var selposition = SelectorElement.rectTransform.position;
          
            selposition.y -= GapBetweenOptions; 
            


            SelectorElement.rectTransform.position = selposition;

            iterations = (iterations + 1) % NUMBEROFSELECTIONS;

            if (iterations == 0)
            {
                selposition.y = InitialSelectorPosition;
                SelectorElement.rectTransform.position = selposition;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            wasset = true;
            iterations--;

            if (iterations <0 ) {
                iterations = NUMBEROFSELECTIONS - 1;
            }

            var selposition = SelectorElement.rectTransform.position;

       
            selposition.y =positions[iterations];

            SelectorElement.rectTransform.position = selposition;
          


        }

        if (wasset) {

            //Debug.Log(" iterations  "+iterations);
            //Debug.Log(" SelectorElement.rectTransform.position " + SelectorElement.rectTransform.position);
        }
      

    }


    //  -SHOWINTRO
    public void InitIntro() {

    }

    /// <summary>
    /// This mehtod should be called to load the game scene 
    /// depending on the selected game mode
    /// 
    /// The game scene should be loading asynchronously
    /// </summary>
    public void LoadGameScene()
    {

    }

    /// <summary>
    /// This method sould be called when the player select
    /// the option of controls
    /// </summary>
    public void LoadControls() {

    }



  

}
