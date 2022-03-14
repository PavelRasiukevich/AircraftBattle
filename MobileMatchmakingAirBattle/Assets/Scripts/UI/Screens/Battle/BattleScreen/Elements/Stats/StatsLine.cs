using TMPro;
using UnityEngine;

public class StatsLine : MonoBehaviour
{
    [SerializeField] private TMP_Text _nickName;
    [SerializeField] private TMP_Text _fragsText;
    [SerializeField] private TMP_Text _failsText;
    public int ActorNumber { get; set; } = 0;

    public void Config(int actorNumber, string nickName, int frags, int fails)
    {
        ActorNumber = actorNumber;
        _nickName.text = nickName;
        _fragsText.text = frags.ToString();
        _failsText.text = fails.ToString();
    }

    public void ConfigFail(int fails)
    {
        _failsText.text = fails.ToString();
    }

    public void ConfigFrags(int frags)
    {
        _fragsText.text = frags.ToString();
    }
}