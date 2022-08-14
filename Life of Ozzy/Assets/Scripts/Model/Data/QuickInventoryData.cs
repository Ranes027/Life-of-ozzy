using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LifeOfOzzy.UI;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Model
{
    public class QuickInventoryData : IDisposable
    {
        private readonly PlayerData _data;

        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntObservableProperty SelectedIndex = new IntObservableProperty();

        public event Action OnChanged;

        public InventoryItemData SelectedItem
        {
            get
            {
                if (Inventory.Length > 0 && Inventory.Length > SelectedIndex.Value)
                    return Inventory[SelectedIndex.Value];

                return null;
            }
        }

        public ItemDefinition SelectedDef => DefinitionsFacade.I.Items.Get(SelectedItem?.Id);

        public QuickInventoryData(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {

            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length);
            OnChanged?.Invoke();

        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged -= OnChangedInventory;
        }
    }

}
