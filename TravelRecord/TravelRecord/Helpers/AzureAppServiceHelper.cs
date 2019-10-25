// Set project to have certain code path included in the build
#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TravelRecord.Helpers
{
    public class AzureAppServiceHelper
    {
        /// <summary>
        /// Call from the first page that we ever load
        /// In this example we'll call from HistoryPage
        /// </summary>
        /// <returns></returns>
        public static async Task SyncAsync()
        {
            // Define certain type of errors
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                // Push any changes from local database to cloud database
                await App.MobileService.SyncContext.PushAsync();

                // Get all information from cloud database to local database
                await App.postsTable.PullAsync("userPosts", "");
            }
            catch (MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                {
                    // If PushResult is different than null, than we have collection of errors
                    syncErrors = mspfe.PushResult.Errors;
                }
            }
            catch (Exception ex)
            {

            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    // Revert update to previous state
                    if (error.OperationKind==MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }
    }
}
