using UnityEngine;
/* GameManager.cs
 * 게임 상태를 관리하는 스크립트
 * 전략 패턴을 사용하여 상태에 따라 UI를 변경
 */
public interface IState
{
    void Enter();
    void Exit();
}
public class LoginState : IState
{
    public void Enter()
    {
        AuthUI.instance.loginPanel.SetActive(true);
        AuthUI.instance.ClearInputFields();
    }
    public void Exit()
    {
        AuthUI.instance.loginPanel.SetActive(false);
    }
}
public class SignUpState : IState
{
    public void Enter()
    {
        AuthUI.instance.signUpPanel.SetActive(true);
        AuthUI.instance.ClearInputFields();
    }
    public void Exit()
    {
        AuthUI.instance.signUpPanel.SetActive(false);
    }
}
public class MainState : IState
{
    public void Enter()
    {
        MainUI.instance.mainPanel.SetActive(true);
    }
    public void Exit()
    {
        MainUI.instance.mainPanel.SetActive(false);
    }
}
public class InGameState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class GameManager : MonoBehaviour
{
#region Private Variables
    private static GameManager instance = null;
    private IState gameState;
#endregion

#region Public Variables
    public static GameManager Instance { get { return instance; } }
#endregion

#region Private Methods
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<OutGameManager>();
        }
    }
    private void Start()
    {
        ChangeState(new LoginState());
    }
#endregion

#region Public Methods
    public void ChangeState(IState newState)
    {
        if (gameState != null) {
            gameState.Exit();
        }
        gameState = newState;
        gameState.Enter();
        Debug.Log($"현재 상태: {gameState.GetType().Name}");
    }
#endregion
}