﻿using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

public class MySyncHandler : IMobileServiceSyncHandler
{
    
    //IMobileServiceClient client;

    public MySyncHandler(/*IMobileServiceClient client*/)
    {
	    //this.client = client;
	    
    }

public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
{
	JObject result = null;
	MobileServicePreconditionFailedException conflictError = null;
	do
	{
		try
		{
			result = await operation.ExecuteAsync();
		}
		catch (MobileServicePreconditionFailedException e)
		{
			conflictError = e;
		}

		if (conflictError != null)
		{
			// There was a conflict on the server. Let's "fix" it by
			// forcing the client entity
			JObject serverItem = conflictError.Value;

			// In most cases, the server will return the server item in the request body
			// when a Precondition Failed is returned, but it's not guaranteed for all
			// backend types.
			if (serverItem == null)
			{
				serverItem = (JObject)(await operation.Table.LookupAsync((string)operation.Item[MobileServiceSystemColumns.Id]));
			}

			// Now update the local item with the server version
			operation.Item[MobileServiceSystemColumns.Version] = serverItem[MobileServiceSystemColumns.Version];
		}
	} while (conflictError != null);

	return result;
}

    public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
    {
	    return Task.FromResult(0);
    }
}