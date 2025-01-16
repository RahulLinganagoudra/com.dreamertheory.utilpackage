using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using DT_Util;

namespace DT.UI.Core
{
    public class TabGroup : Menu
    {
        public enum ButtonStateType
        {
            SpriteSwap,
            ColorChange,
            None
        }
        public delegate void TabGroupChangedEvent(int index);
        public event TabGroupChangedEvent OnTabGroupChanged;
        public event TabGroupChangedEvent OnTabGroupSelected;
        //assign it in debug mode to script
        [SerializeField] TabButton tabButtonPrefab;
        [SerializeField] Transform navbarTabContainer;
        public List<TabButton> tabButtons;
        [SerializeField] ButtonStateType buttonStateType;
        public Sprite idle, hovered, selected;
        public Color idleColor, hoveredColor, selectedColor;
        public TabButton selectedTab;
        public Transform panelParent;
        public List<GameObject> panels;
        private int currentPanelIndex;

        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
                tabButtons = new List<TabButton>();
            if (!tabButtons.Contains(button))
                tabButtons.Add(button);
            if (selectedTab == null)
            {
                OnTabSelected(tabButtons[0]);
            }
            else
            {
                ResetTabs();
            }

        }
        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            {
                if (buttonStateType == ButtonStateType.SpriteSwap)
                    button.bg.sprite = hovered;

                else if (buttonStateType == ButtonStateType.ColorChange)
                {
                    button.bg.color = hoveredColor;
                }
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }
        public void OnTabSelected(TabButton button)
        {
            GameObject go;
            if (selectedTab != null)
            {
                if (selectedTab.id == button.id)
                {
                    selectedTab = button;
                    selectedTab.Select();
                    ResetTabs();


                    if (buttonStateType == ButtonStateType.SpriteSwap)
                        button.bg.sprite = selected;

                    else if (buttonStateType == ButtonStateType.ColorChange)
                    {
                        button.bg.color = selectedColor;
                    }

                    OnTabGroupSelected?.Invoke(selectedTab.id);
                    return;
                }
                selectedTab.Deselect();
                go = panels[selectedTab.id];
                go.SetActive(false);
            }
            selectedTab = button;
            selectedTab.Select();

            currentPanelIndex = selectedTab.id;

            ResetTabs();



            if (buttonStateType == ButtonStateType.SpriteSwap)
                button.bg.sprite = selected;

            else if (buttonStateType == ButtonStateType.ColorChange)
            {
                button.bg.color = selectedColor;
            }



            go = panels[currentPanelIndex];

            go.SetActive(true);
            OnTabGroupSelected?.Invoke(selectedTab.id);
            OnTabGroupChanged?.Invoke(selectedTab.id);
            //for (int i = 0; i < panels.Count; i++)
            //{
            //    if (i == currentPanelIndex)
            //    {
            //        panels[i].gameObject.SetActive(true);
            //        panels[i].Open();
            //    }
            //    else
            //    {
            //        panels[i].gameObject.SetActive(false);
            //    }
            //}
        }
        public void AddOnlyTabButtons(int id, string name = "", UnityAction action = null)
        {
            AddTabBtn(id, name, action);

            OnTabGroupChanged?.Invoke(selectedTab.id);

        }
        public void DeleteNavButtons()
        {
            tabButtons.Clear();
            navbarTabContainer.DestroyChildren();
        }
        private void AddTabBtn(int id, string name, UnityAction action)
        {
            var tabButton = Instantiate(tabButtonPrefab, navbarTabContainer);
            tabButtons.Add(tabButton);
            tabButton.id = id;
            tabButton.Name = name;
            if (action != null)
                tabButton.onTabSelected.AddListener(action);
            tabButton.tabGroup = this;
            OnTabSelected(tabButton);
        }
        public GameObject AddTab(GameObject uiPrefab, string name, UnityAction callback = null)
        {
            var panel = Instantiate(uiPrefab, panelParent);
            panels.Add(panel);
            AddTabBtn(navbarTabContainer.childCount - 1, name, callback);
            OnTabGroupChanged?.Invoke(selectedTab.id);
            return panel;
        }
        public void RemoveTab(GameObject panelInstance)
        {
            if (panelInstance == null || !panels.Contains(panelInstance)) return;
            int indexOfPanel = panels.IndexOf(panelInstance);
            panels.RemoveAt(indexOfPanel);
            tabButtons.RemoveAt(indexOfPanel);
            OnTabSelected(tabButtons[indexOfPanel % panels.Count]);
            OnTabGroupChanged?.Invoke(selectedTab.id);

        }
        public void ResetTabs()
        {
            foreach (var button in tabButtons)
            {
                if (selectedTab != null && button == selectedTab)
                    continue;
                else
                {
                    if (buttonStateType == ButtonStateType.SpriteSwap)
                        button.bg.sprite = idle;

                    else if (buttonStateType == ButtonStateType.ColorChange)
                    {
                        button.bg.color = idleColor;
                    }
                }
            }
            OnTabGroupChanged?.Invoke(selectedTab.id);

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OpenPreviousTab();

            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                OpenNextTab();
            }
        }

        public void OpenPreviousTab()
        {
            currentPanelIndex = (currentPanelIndex - 1 + panels.Count) % panels.Count;

            OnTabSelected(tabButtons[currentPanelIndex]);
        }

        public void OpenNextTab()
        {
            currentPanelIndex = (currentPanelIndex + 1) % panels.Count;
            OnTabSelected(tabButtons[currentPanelIndex]);
        }
        public enum Tab
        {
            Inventory = 0,
            Crafting = 1,
            SkillTree = 2,
            Lore = 3,
            Map = 4
        }
        public void Open(Tab tab)
        {
            if (!opened)
                Open();
            OnTabSelected(tabButtons[(int)tab]);
        }
        public override void Open()
        {
            base.Open();
            Time.timeScale = 0;
            foreach (var panel in panels)
            {
                panel.gameObject.SetActive(false);
            }
            OnTabSelected(tabButtons[0]);
        }
        public override void Close()
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
                var go = panels[tabButtons.IndexOf(selectedTab)];
                go.SetActive(false);
            }
            selectedTab = null;
            Time.timeScale = 1;

            base.Close();
        }
    }
}