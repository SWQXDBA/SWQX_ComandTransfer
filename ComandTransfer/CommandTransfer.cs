using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using test;
using Torch;
using Torch.API;

namespace ComandTransfer
{
    public class CTPlugin : TorchPluginBase {
        private Persistent<Config> _config;
        public Config Config => _config?.Data;
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static CTPlugin Static;
        public List<MyKeyValuePair<string, string>>  Transfers => Config.TransferInfos;
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
        }
        
    }
}