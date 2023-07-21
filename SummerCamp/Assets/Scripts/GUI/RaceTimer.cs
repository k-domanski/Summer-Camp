using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI raceTimer;
    [SerializeField]
    private TextMeshProUGUI countdownTimer;
    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        gameManager.IsRunning.ValueChanged += OnIsRunningValueChanged;
        gameManager.IsStarting.ValueChanged += OnIsStartingValueChanged;
        gameManager.CurrentTime.ValueChanged += HandleTimeValueChanged;
    }

    private void OnDestroy()
    {
        gameManager.IsRunning.ValueChanged -= OnIsRunningValueChanged;
        gameManager.IsStarting.ValueChanged -= OnIsStartingValueChanged;
        gameManager.CurrentTime.ValueChanged -= HandleTimeValueChanged;
    }

    private void HandleTimeValueChanged(float lastvalue, float newvalue)
    {
        SetTimers(newvalue);
    }

    private void OnIsRunningValueChanged(bool lastvalue, bool newvalue)
    {
        raceTimer.gameObject.SetActive(newvalue);
    }

    private void OnIsStartingValueChanged(bool lastvalue, bool newvalue)
    {
        countdownTimer.gameObject.SetActive(newvalue);
    }

    private void SetTimers(float newValue)
    {
        if (raceTimer.gameObject.activeSelf)
        {
            raceTimer.text = newValue.ToString("F");
        }

        if (countdownTimer.gameObject.activeSelf)
        {
            countdownTimer.text = newValue.ToString("0");
        }
    }
}
