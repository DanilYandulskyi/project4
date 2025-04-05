using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GoldUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        UpdateText(0);
    }

    public void UpdateText(int goldAmount)
    {
        _text.text = $"Gold - {goldAmount}";
    }
}
