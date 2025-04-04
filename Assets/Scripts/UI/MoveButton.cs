using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    [SerializeField] private Text moveNameText;
    [SerializeField] private Text valorCostText;

    public void Initialize(ActionCommandBase move)
    {
        moveNameText.text = move.name;
        valorCostText.text = move.name;
        //GetComponent<Button>().onClick.AddListener(() => BattleManager.Instance.SelectMove(move));
    }
}
