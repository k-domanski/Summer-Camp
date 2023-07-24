using UnityEngine;
using Elympics;

[RequireComponent(typeof(InputProvider))]
public class InputController : ElympicsMonoBehaviour, IInputHandler, IInitializable, IUpdatable
{
    [SerializeField] private MovementController movementController = null;
    [SerializeField] private SkillController skillController = null;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float additionalRaycastDistance = 0.5f;
    
    private InputProvider inputProvider = null;
    private PlayerData playerData = null;
    private float raycastDistance;
    
    private void Start()
    {
        raycastDistance = Vector3.Magnitude(this.transform.position - playerCamera.transform.position) + additionalRaycastDistance;
    }

    #region IUpdatable
    public void ElympicsUpdate()
    {
        float horizontalMovement = 0.0f;
        float verticalMovement = 0.0f;
        float worldLookPosX = 0f;
        float worldLookPosZ = 0f;
        bool isFire = false;

        if (ElympicsBehaviour.TryGetInput(ElympicsPlayer.FromIndex(playerData.PlayerID), out var inputReader))
        {
            inputReader.Read(out horizontalMovement);
            inputReader.Read(out verticalMovement);
            inputReader.Read(out worldLookPosX);
            inputReader.Read(out worldLookPosZ);
            inputReader.Read(out isFire);

            ProcessInput(new Vector3(horizontalMovement,0, verticalMovement),new Vector3(worldLookPosX,0,worldLookPosZ));
            skillController.ProcessInput(isFire);
        }
    }
    #endregion

    #region IInitializable
    public void Initialize()
    {
        this.inputProvider = GetComponent<InputProvider>();
        this.playerData = GetComponent<PlayerData>();
    }
    #endregion

    #region IInputHandler
    public void OnInputForBot(IInputWriter inputSerializer)
    {
        SerializeInput(inputSerializer);
    }

    public void OnInputForClient(IInputWriter inputSerializer)
    {
        SerializeInput(inputSerializer);
    }
    #endregion

    private void SerializeInput(IInputWriter inputWriter)
    {
        inputWriter.Write(inputProvider.Movement.x);
        inputWriter.Write(inputProvider.Movement.y);

        var v3mousePos = new Vector3(inputProvider.MousePos.x, inputProvider.MousePos.y, raycastDistance);
        var worldPos = playerCamera.ScreenToWorldPoint(v3mousePos);
        
        inputWriter.Write(worldPos.x);
        inputWriter.Write(worldPos.z);

        inputWriter.Write(inputProvider.IsFire);
    }

    private void ProcessInput(Vector3 movement, Vector3 rotation)
    {
        movementController.ProcessInput(movement, rotation);
    }
}
