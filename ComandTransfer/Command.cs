using NLog;
using test;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace ComandTransfer
{

    public class Command : CommandModule
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public CTPlugin Plugin => (CTPlugin)Context.Plugin;

        public Config Config => Plugin.Config;
        [Command("addtsf", "Set up a pair of instruction translations")]
        [Permission(MyPromoteLevel.Admin)]
        public void cmdtsf(string from,string to)
        {
            foreach (var pair in CTPlugin.Static.Transfers)
            {
                if (pair.Key.Equals(from))
                {
                    pair.Value = to;
                    break;
                }
            }
            CTPlugin.Static.Transfers.Add(new MyKeyValuePair<string, string>(from,to));
            CTPlugin.Static.saveConfig();
            Context.Respond("done!");
        }
        [Command("alltsf", "show all transfer pairs")]
        [Permission(MyPromoteLevel.Admin)]
        public void alltsf()
        {
            if (CTPlugin.Static.Transfers.Count == 0)
            {
                Context.Respond("empty!");
                return;
            }
            CTPlugin.Static.Transfers.ForEach(pair =>
            {
                Context.Respond($"{pair.Key} is transfer to {pair.Value}");
            });
        }
        [Command("deletetsf", "delete a transfer pair by key")]
        [Permission(MyPromoteLevel.Admin)]
        public void delete(string key)
        {
            foreach (var pair in CTPlugin.Static.Transfers)
            {
                //because key has been transfer to value
                if (pair.Value.Equals(key))
                {
                    CTPlugin.Static.Transfers.Remove(pair);
                    Context.Respond($"Cancelled {pair.Key} transfer to {pair.Value}");
                    CTPlugin.Static.saveConfig();
                    return;
                }
            }
            Context.Respond("Can't found the given key");
        }
    }
}