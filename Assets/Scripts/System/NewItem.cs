using UnityEngine;
/* Item.cs
 * 아이템을 관리하는 스크립트
 * 아이템은 필드 or 인벤토리에 있음
 * 필드에 있는 아이템 클릭 -> 인벤토리에 추가
 * 인벤토리에 있는 아이템 클릭하여 사용
 * 인벤토리에 있는 아이템은 이미지와 이름만 있으면 됨
 */
public class NewItem : MonoBehaviour
{
    [HideInInspector] public Define.ItemName itemName = Define.ItemName.None;
    public Sprite icon = null;

    public void InitializeItem()
    {
        string path = this.GetComponentInParent<RoomInfo>().roomPath;
        icon = Resources.Load<Sprite>($"Sprites/Items/{path}/{this.name}");
        if (System.Enum.TryParse<Define.ItemName>(this.name, out var itemName)) {
            this.itemName = itemName;
        }
        else {
            Debug.LogError($"{this.name} 아이템의 이름을 Enum으로 파싱할 수 없습니다.");
            this.itemName = Define.ItemName.None;
        }
    }
}