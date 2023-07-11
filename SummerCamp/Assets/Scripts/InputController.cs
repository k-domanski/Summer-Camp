using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

[RequireComponent(typeof(InputProvider))]
public class InputController : ElympicsMonoBehaviour, IInputHandler, IInitializable, IUpdatable
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private MovementController movementController = null;

    private InputProvider inputProvider = null;

    #region IUpdatable
    public void ElympicsUpdate()
    {
        float horizontalMovement = 0.0f;
        float verticalMovement = 0.0f;

        if (ElympicsBehaviour.TryGetInput(ElympicsPlayer.FromIndex(playerId), out var inputReader))
        {
            inputReader.Read(out horizontalMovement);
            inputReader.Read(out verticalMovement);

            ProcessMovement(horizontalMovement, verticalMovement);
        }

    }
    #endregion

    #region IInitializable
    public void Initialize()
    {
        this.inputProvider = GetComponent<InputProvider>();
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
    }

    private void ProcessMovement(float horizontal, float vertical)
    {
        movementController.ProcessMovement(horizontal, vertical);
    }
}
