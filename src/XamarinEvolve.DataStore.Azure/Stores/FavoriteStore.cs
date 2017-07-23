using System;
using System.Threading.Tasks;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;

using Xamarin.Forms;
using System.Linq;
using XamarinEvolve.DataStore.Azure;
using System.Threading;
using System.Collections.Generic;

namespace XamarinEvolve.DataStore.Azure
{
    public class FavoriteStore : BaseStore<Favorite>, IFavoriteStore
    {
        public async  Task<bool> IsFavorite(string sessionId)
        {
            await InitializeStore().ConfigureAwait (false);
            var items = await Table.Where(s => s.SessionId == sessionId).ToListAsync().ConfigureAwait (false);
            return items.Count > 0;
        }

        public Task DropFavorites()
        {
            return Task.FromResult(true);
        }

        public override string Identifier => "Favorite";

        public override async Task<bool> RemoveAsync(Favorite item)
		{
            await StoreManager.MobileService.SyncContext.Store.DeleteAsync(Identifier, new List<string>() { item.Id });
            //await Table.PurgeAsync<Favorite>(null,Table.Where(x => x.SessionId == item.SessionId),default(CancellationToken));                           
            /*
            var table = StoreManager.MobileService.GetTable<Favorite>();
			var favorite = (await Table.Where(x => x.SessionId == item.SessionId)
			 .Take(1)
			 .ToEnumerableAsync()).FirstOrDefault();
			await table.DeleteAsync(favorite);
			*/
            /*
			await InitializeStore().ConfigureAwait(false);
			await PullLatestAsync().ConfigureAwait(false);
			await SyncAsync().ConfigureAwait(false);
            /*
			var favorite = (await Table.Where(x => x.SessionId == item.SessionId)
	        .Take(1)
	        .ToEnumerableAsync()).FirstOrDefault();
            await Table.DeleteAsync(item);
			*/
		
			return true;
		}


	}
}

