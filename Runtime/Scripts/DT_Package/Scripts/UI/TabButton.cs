using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace DT.UI.Core
{
    [RequireComponent(typeof(Image))]
    public class TabButton : DataButton
    {
        public TabGroup tabGroup;
        [HideInInspector] public Image bg;
        TextMeshProUGUI text;
        public string Name
        {
            get
            {
                return text.text;
            }
            set
            {
                text??= GetComponentInChildren<TextMeshProUGUI>();
                name = value;
                text.text = value;
            }
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            Select();
            tabGroup.OnTabSelected(this);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
            print("enter");
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }

        private void Start()
        {
            bg = GetComponent<Image>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            tabGroup.Subscribe(this);

        }
        public void Select() { onTabSelected?.Invoke(); }
        public void Deselect() { onTabDeselected?.Invoke(); }

    }
}