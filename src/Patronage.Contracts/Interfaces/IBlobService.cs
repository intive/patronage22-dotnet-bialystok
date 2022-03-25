using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace Patronage.Contracts.Interfaces
{
    public interface IBlobService
    {
        Task GetBlobAsync(string blobContainerName, string localDirectory);
    }
}