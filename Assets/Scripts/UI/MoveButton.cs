using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    [SerializeField] private Text moveNameText;
    [SerializeField] private Text valorCostText;

    public void Initialize(MoveBase move)
    {
        moveNameText.text = move.Name;
        valorCostText.text = move.ValorCost.ToString();
        GetComponent<Button>().onClick.AddListener(() => BattleManager.Instance.SelectMove(move));
    }
}
