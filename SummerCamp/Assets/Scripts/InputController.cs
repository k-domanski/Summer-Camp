using System.Text;
using UnityEngine;
using Elympics;

[RequireComponent(typeof(InputProvider))]
public class InputController : ElympicsMonoBehaviour, IInputHandler, IInitializable, IUpdatable
{
    [SerializeField] private MovementController movementController = null;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float additionalRaycastDistance = 0.5f;

    private InputProvider inputProvider = null;
    private PlayerData playerData = null;
    private HUDController HUDController = null;
    private float raycastDistance;

    private Vector3 hitPoint;

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
        bool isSkillActive = false;
        bool isSecondaryActive = false;

        float worldPosX = 0.0f;
        float worldPosZ = 0.0f;

        bool showScoreboard = false;
       
        if (ElympicsBehaviour.TryGetInput(ElympicsPlayer.FromIndex(playerData.PlayerID), out var inputReader))
        {
            inputReader.Read(out horizontalMovement);
            inputReader.Read(out verticalMovement);
            inputReader.Read(out worldLookPosX);
            inputReader.Read(out worldLookPosZ);
            inputReader.Read(out isFire);
            inputReader.Read(out isSkillActive);
            inputReader.Read(out isSecondaryActive);


            inputReader.Read(out worldPosX);
            inputReader.Read(out worldPosZ);

            inputReader.Read(out showScoreboard);

            ProcessInput(new Vector3(horizontalMovement,0, verticalMovement),new Vector3(worldLookPosX,0,worldLookPosZ));

            playerData.UsePrimary(isFire, isSkillActive, new Vector3(worldPosX, 0, worldPosZ));
            playerData.UseSecondary(isFire, isSecondaryActive, new Vector3(worldPosX, 0, worldPosZ));

            HUDController.ProcessHUDActions(showScoreboard);

        }
    }
    #endregion

    #region IInitializable
    public void Initialize()
    {
        this.inputProvider = GetComponent<InputProvider>();
        this.playerData = GetComponentInChildren<PlayerData>();
        playerCamera = Camera.main;
        this.HUDController = GetComponent<HUDController>();
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

        inputWriter.Write(inputProvider.FireSkill);
        inputWriter.Write(inputProvider.ActivePrimarySkill);
        inputWriter.Write(inputProvider.ActiveSecondarySkill);
        
        
        Vector3 worldPosition = CalculateScreenToWorld(inputProvider.MousePos);
        inputWriter.Write(worldPosition.x);
        inputWriter.Write(worldPosition.z);

        inputWriter.Write(inputProvider.ShowScoreboard);
    }

    private Vector3 CalculateScreenToWorld(Vector2 mousePosition)
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hitPoint = hit.point;
        }
        return hitPoint;
    }

    private void ProcessInput(Vector3 movement, Vector3 rotation)
    {
        movementController.ProcessInput(movement, rotation);
    }
}
