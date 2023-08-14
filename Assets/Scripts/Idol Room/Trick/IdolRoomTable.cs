using UnityEngine;
using static Define;

public class IdolRoomTable : Trick
{
    [Header("���� ������")]
    public GameObject musicBoxOnTable;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.MusicBox))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
                Inventory.Instance.RemoveItem(ItemName.MusicBox);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }
    public override void SolvedAction()
    {
        musicBoxOnTable.SetActive(true);
    }
}
