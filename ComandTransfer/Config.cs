﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using test;
using Torch;

namespace ComandTransfer
{
    public class Config : ViewModel
    {
        private List<MyKeyValuePair<string, string>> transferInfos_ = new List<MyKeyValuePair<string, string>>();

        public ObservableCollection<Convertor> convertors{ get; }  = new ObservableCollection<Convertor>();

        public Config()
        {
            convertors.CollectionChanged += (sender, args) => OnPropertyChanged();
        }

        public   List<MyKeyValuePair<string, string>> TransferInfos
        {
            get => transferInfos_;
            set => SetValue(ref transferInfos_, value);
        }
    }
}