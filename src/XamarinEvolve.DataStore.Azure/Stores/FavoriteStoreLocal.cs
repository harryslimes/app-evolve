using System;
using System.Threading.Tasks;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;

using Xamarin.Forms;
using System.Linq;
using XamarinEvolve.DataStore.Azure;
using System.Threading;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;

namespace XamarinEvolve.DataStore.Azure
{
	public class FavoriteStoreLocal : IFavoriteStore
	{
        IStoreManager storeManager;
        IMobileServiceLocalStore localStore;
		public async Task InitializeStore()
		{
			if (storeManager == null)
				storeManager = DependencyService.Get<IStoreManager>();
            if (localStore == null)
            {
                localStore = StoreManager.MobileService.SyncContext.Store;
            }

			if (!storeManager.IsInitialized)
				await storeManager.InitializeAsync().ConfigureAwait(false);
		}

		public async Task<bool> IsFavorite(string sessionId)
		{
			await InitializeStore().ConfigureAwait(false);
			var items = await Table.Where(s => s.SessionId == sessionId).ToListAsync().ConfigureAwait(false);
			return items.Count > 0;
		}

        IMobileServiceSyncTable<Favorite> table;
		protected IMobileServiceSyncTable<Favorite> Table
		{
			get { return table ?? (table = StoreManager.MobileService.GetSyncTable<Favorite>()); }

		}

		public Task DropFavorites()
		{
			return Task.FromResult(true);
		}

		public string Identifier => "Favorite";

		public async Task<bool> RemoveAsync(Favorite item)
		{
            await localStore.DeleteAsync(Identifier, new List<string>() { item.Id });
	        return true;
		}

        public async Task<IEnumerable<Favorite>> GetItemsAsync(bool forceRefresh = true)
        {
			await InitializeStore().ConfigureAwait(false);
			if (forceRefresh)
				await PullLatestAsync().ConfigureAwait(false);

			return await Table.ToEnumerableAsync().ConfigureAwait(false);
        }

        public Task<Favorite> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> InsertAsync(Favorite item)
		{
			await InitializeStore().ConfigureAwait(false);
			await Table.InsertAsync(item).ConfigureAwait(false);
			return true;
		}

        public Task<bool> UpdateAsync(Favorite item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public void DropTable()
        {
			table = null;
        }

		public async Task<bool> PullLatestAsync()
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				Debug.WriteLine("Unable to pull items, we are offline");
				return false;
			}
			try
			{
				await Table.PullAsync($"all{Identifier}", Table.CreateQuery()).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to pull items, that is alright as we have offline capabilities: " + ex);
				return false;
			}
			return true;
		}

	}
}

