using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{

    public interface IBasketItemService
    {
        Task AddBasketItem(BasketItem basketItem);
        void DeleteBasketItem(int id, string userId);
        List<BasketItem> GetAllBasketItems(Func<BasketItem, bool>? func = null);
        BasketItem GetBasketItem(Func<BasketItem, bool>? func = null);
        void UpdateBasketItem(int id, BasketItem newBasketItem);


    }
}
