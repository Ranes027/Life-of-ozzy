using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LifeOfOzzy.Model
{
    [Serializable]
    public class ShopData
    {
        [SerializeField] private List<AmountOfGoods> _counts;        

        public int GetAmount(string id)
        {
            var def = DefinitionsFacade.I.Shop.Get(id);
            var counts = _counts.FirstOrDefault(x => x.Id == id);
            if(counts == null)
                return def.Amount;
            return counts.Count;
        }

        public void DecreaseAmount(string id)
        {
            var def = DefinitionsFacade.I.Shop.Get(id);
            var counts = _counts.FirstOrDefault(x => x.Id == id);
            if(counts == null)
                _counts.Add(new AmountOfGoods(id, def.Amount -1));
            else
                counts.Count--;
        }
    }

    [Serializable]
    public class AmountOfGoods
    {
        public string Id;
        public int Count;

        public AmountOfGoods(string id, int count)
        {
            Id = id;
            Count = count;
        }
    }

}
