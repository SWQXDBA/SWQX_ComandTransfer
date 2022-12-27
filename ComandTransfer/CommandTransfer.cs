using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using NLog;
using test;
using Torch;
using Torch.API;
using Torch.API.Plugins;

namespace ComandTransfer
{
    public class CTPlugin : TorchPluginBase,IWpfPlugin {
        private Persistent<Config> _config;
        public Config Config => _config?.Data;
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static CTPlugin Static;
        public UserControl GetControl() => _control ??= new MainWindow(this);
        private  MainWindow _control;
        private void SetupConfig() {

            var configFile = Path.Combine(StoragePath, "CommandTransfer.cfg");

            try {

                _config = Persistent<Config>.Load(configFile);

            } catch (Exception e) {
                Log.Warn(e);
            }

            if (_config?.Data == null) {

                Log.Info("Create Default Config, because none was found!");

                _config = new Persistent<Config>(configFile, new Config());
                _config.Save();
            }
        }

        public void saveConfig()
        {
            _config.Save();
        }
        
        public override void Init(ITorchBase torch) {
            base.Init(torch);
            SetupConfig();
            Static = this;
            fixOldData();
        }

   

        private void fixOldData()
        {
            foreach (var myKeyValuePair in Config.TransferInfos)
            {
                Convertor c = new Convertor();
                c.From = myKeyValuePair.Key;
                c.To = myKeyValuePair.Value;
                if (!Config.convertors.Contains(c))
                {
                    Config.convertors.Add(c);
                }
            }
            Config.TransferInfos.Clear();
            this.saveConfig();
        }
    }
}