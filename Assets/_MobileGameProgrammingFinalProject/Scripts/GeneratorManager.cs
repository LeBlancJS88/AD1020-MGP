using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private StressManager stressManager;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private GameObject stressGeneratorLevelOneButton;
    [SerializeField] private GameObject stressGeneratorLevelTwoButton;

    void Update()
    {
        
    }

    private void StressGeneratorLevelOneClick() {         
        
        if (stressManager.stressMaximumValue >= 1000)
        {
            
        }
    }
}
