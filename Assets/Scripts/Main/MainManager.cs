using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    // 새로하기 버튼 클릭 시 DB 초기화
    public void OnClickNewGameBtn()
    {
        DatabaseManager.Instance.ResetData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }

    // 불러오기 버튼 클릭 시 DB에 저장된 트릭/아이템 정보 불러오기
    public void OnClickLoadGameBtn()
    {
        DatabaseManager.Instance.GetUserData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }

    // 설정 버튼 클릭 시
    public void OnClickSettingBtn()
    {

    }

    // 로그아웃 버튼 클릭 시
    public void OnClickLogOutBtn()
    {

    }

}
