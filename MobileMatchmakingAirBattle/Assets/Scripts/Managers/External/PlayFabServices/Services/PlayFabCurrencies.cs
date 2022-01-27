using Assets.Scripts.Utils;
using PlayFab;
using PlayFab.ClientModels;
using TO;
using UnityEngine;

namespace Managers.External.PlayFabServices.Services
{
    public class PlayFabCurrencies
    {
        #region PUBLIC

        public void Subtract(int amount)
        {
            PlayFabClientAPI.SubtractUserVirtualCurrency(
                new SubtractUserVirtualCurrencyRequest
                {
                    Amount = amount,
                    VirtualCurrency = Const.Currencies.Gold
                },
                result => User.Currency.CountUpdate(result.Balance),
                error => Debug.LogError(error.ErrorMessage)
            );
        }

        public void Add(int amount)
        {
            PlayFabClientAPI.AddUserVirtualCurrency(
                new AddUserVirtualCurrencyRequest
                {
                    Amount = amount,
                    VirtualCurrency = Const.Currencies.Gold
                },
                result => User.Currency.CountUpdate(result.Balance),
                error => Debug.LogError(error.ErrorMessage)
            );
        }

        #endregion
    }
}