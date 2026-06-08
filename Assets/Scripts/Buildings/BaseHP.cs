using TMPro;
using UnityEngine;

public class BaseHP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BaseHPTxt;
    private Building myBase;

    bool isInitialized = false;


    // ----------------------------------------------------------------------------------------------------------------
    public void Initialize(Building myBase)
    {

        this.myBase = myBase;
        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized)
        {
            return;
        }
        else
        {
            BaseHPTxt.text = myBase.getCurrentHp().ToString();

        }
    }
    // ----------------------------------------------------------------------------------------------------------------

}
