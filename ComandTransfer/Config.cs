using System.Collections.Generic;
using test;
using Torch;

namespace ComandTransfer
{
    public class Config : ViewModel
    {
        private List<MyKeyValuePair<string, string>> transferInfos_ = new List<MyKeyValuePair<string, string>>();

        
        public   List<MyKeyValuePair<string, string>> TransferInfos
        {
            get => transferInfos_;
            set => SetValue(ref transferInfos_, value);
        }
    }
}