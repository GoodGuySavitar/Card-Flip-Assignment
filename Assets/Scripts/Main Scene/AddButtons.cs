using UnityEngine;
using UnityEngine.UI;

public class AddButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform backgroundPanel;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    private void Awake()
    {
        // SetNoOfRows();
        SpawnButtons();
    }

    // private void SetNoOfRows()
    // {
    //     gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    //     gridLayoutGroup.constraintCount = buttonsManager.gridSize;
    // }

    private void SpawnButtons()
    {
        int gridSize = 4;
        int numOfButtons = gridSize * gridSize;
        
        for(int i = 0; i < numOfButtons; i++)
        {
            GameObject newBtn = Instantiate(buttonPrefab);
            newBtn.transform.SetParent(backgroundPanel, false);
            newBtn.name = "Button_" + i; 
            Button buttonComponent = newBtn.GetComponent<Button>();
        }
    }
}
