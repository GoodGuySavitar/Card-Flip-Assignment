using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    private Dictionary<string, Sprite> buttonImageMap = new Dictionary<string, Sprite>();

    [SerializeField] private GameObject gameScoreText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text noOfTurnsText;

    [SerializeField] private GameObject endGameText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTurnsText;

    private int gameScore; 
    private int noOfTurns; 
    
    private int matchedPairs = 0;
    private int totalPairs;

    private Button firstSelected = null;
    private Button secondSelected = null; 

    private void Start()
    {
        AssignImagesToButtons();
        endGameText.SetActive(false);
        totalPairs = GameObject.FindGameObjectsWithTag("Button").Length / 2;
        Debug.Log(totalPairs);
    }

    private void AssignImagesToButtons()
    {

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        //DUPLICATING THE LIST OF IMAGES
        List<Sprite> shuffledImages = new List<Sprite>(images);
        shuffledImages.AddRange(images);
        
        //CALL THE SHUFFLE LIST FUNCTION USING THE SHUFFLED IMAGES LIST
        ShuffleList(shuffledImages);

        //CONNECTING IMAGES TO THE CARDS
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonImageMap[buttons[i].name] = shuffledImages[i];
            Button btn = buttons[i].GetComponent<Button>();
            
            //ADDING LISTENERS TO BUTTONS
            if (btn != null)
            {
                btn.onClick.AddListener(() => ButtonClick(btn));
            }
        }
    }

    public void ButtonClick(Button clickedButton)
    {
        //MAKE SURE THAT WHEN TWO CARDS ARE SELECTED, PLAYER CANNOT SELECT A THIRD CARD
        if (firstSelected != null && secondSelected != null)
        return; 

        //LOOKS UP THE IMAGE THAT IS MAPPED TO THE BUTTON ON THE DICTIONARY
        string buttonName = clickedButton.name;
        clickedButton.image.sprite = buttonImageMap[buttonName];
        

        if (firstSelected == null)
        {
            firstSelected = clickedButton;
        }
        else if (secondSelected == null && clickedButton != firstSelected)
        {
            secondSelected = clickedButton;
            StartCoroutine(CheckForMatch());
        }
    }

    private IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1f);

        if (buttonImageMap[firstSelected.name] == buttonImageMap[secondSelected.name])
        {
            Debug.Log("CORRECT");
            //PLAYER CANT INTERACT WITH THE BUTTONS ANYMORE
            firstSelected.interactable = false;
            secondSelected.interactable = false;
            gameScore += 5;
            scoreText.text = "SCORE: " + gameScore;
            
            matchedPairs++;            

            if (matchedPairs == totalPairs) // Check if all pairs are found
            {
                Debug.Log("GAMEOVER");
                EndGame();
            }
        }

        else
        {
            Debug.Log("INCORRECT");
            //FLIP THE CARDS BACK BY MAKING THE IMAGE NULL AGAIN. IF THE PLAYER CLICKS ON THE SAME CARD AGAIN THEN THERE IS A CHECK FOR WHICH IMAGE THE CARD IS MAPPED TO IN THE DICTIONARY AND THEN THE SPRITE IS CHANGED AGAIN
            firstSelected.image.sprite = null;
            secondSelected.image.sprite = null;
        }
        
        //RESETTING THE SELECTIONS AND COUNTING THE NUMBER OF TURNS
        firstSelected = null;
        secondSelected = null;
        noOfTurns++;
        noOfTurnsText.text = "TURNS: " + noOfTurns;
        

    }

    private void EndGame()
    {
        finalScoreText.text = "TOTAL SCORE: " + gameScore;
        finalTurnsText.text = "TOTAL TURNS: " + noOfTurns;
        gameScoreText.SetActive(false);
        endGameText.SetActive(true);

        foreach (GameObject buttonObj in GameObject.FindGameObjectsWithTag("Button"))
        {
            Button btn = buttonObj.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = false;
            }
        }
    }

    //FUNCTION TO SHUFFLE THE ITEMS OF THE LIST BY ITERATING THROUGH THE ARRAY AND FINGDING A RANDOM INDEX FOR EACH MEMBER AND SWAPPING THE ELEMENTS
    private void ShuffleList(List<Sprite> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void StartGame()
    {   
        SceneManager.LoadScene(1);
    }
}
