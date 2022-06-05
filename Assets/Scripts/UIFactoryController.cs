using System;
using TMPro;
using UnityEngine;

public class UIFactoryController : MonoBehaviour
{
    [SerializeField]
    private BaseFactory _factory;
    [SerializeField]
    private RectTransform _UIBox;
    [SerializeField]
    private TMP_Text _description;
    [SerializeField]
    private RectTransform _canvas;
    [SerializeField]
    private float _offsetY;

    private void SetText()
    {
        _description.text =  $"Quantity: ";
        string quantity = String.Concat(_factory._outputStorage.CurrentAmount.ToString(), "/", _factory._outputStorage.Capacity.ToString());
        if (_factory._outputStorage.CurrentAmount >= _factory._outputStorage.Capacity)
        {
            quantity = String.Concat("<color=red>", quantity, "</color>");
        }

        if (_factory._needsResource.Count > 0)
        {
            string needsText = "Needs: \n";
            foreach (var storage in _factory._inputStorages)
            {
                string needed = String.Concat(storage.Resource.ToString(), " ",storage.CurrentAmount.ToString(), "/", storage.Capacity.ToString());
                if (storage.CurrentAmount < 1)
                {
                    needed = String.Concat("<color=red>", needed, "</color>");
                }
                needsText = String.Concat(needsText, needed, "\n");
            }
            _description.text = String.Concat(needsText, _description.text);
        }
        _description.text = string.Concat(_description.text, "\n", quantity);
    }

    void Update()
    {
        SetText();
        float offsetPosY = _factory.transform.position.y + _offsetY;
        Vector3 offsetPos = new Vector3(_factory.transform.position.x, offsetPosY, _factory.transform.position.z);
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPoint, null, out canvasPos);
        _UIBox.localPosition = canvasPos;
    }
}
