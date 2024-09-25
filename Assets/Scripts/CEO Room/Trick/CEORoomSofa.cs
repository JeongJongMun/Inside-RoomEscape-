using UnityEngine;
using UnityEngine.UI;

public class CEORoomSofa : Trick
{
    public GameObject sofaRip;
    public GameObject sofaRipZoom;
    public GameObject sofaZoom;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(Define.EItemType.Cutter))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("cutter");
                Inventory.instance.RemoveItem(Define.EItemType.Cutter);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }
    public override void SolvedAction()
    {
        sofaZoom.GetComponent<Image>().raycastTarget = false;
        sofaRip.SetActive(true);
        sofaRipZoom.SetActive(true);
    }
}
