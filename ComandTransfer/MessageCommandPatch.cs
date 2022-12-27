using System;
using System.Reflection;
using Torch.Commands;
using Torch.Managers.PatchManager;

namespace ComandTransfer
{
    [PatchShim]
    public class MessageCommandPatch
    {
        
        internal static readonly MethodInfo updatePatchBefore =
            typeof(MessageCommandPatch).GetMethod(nameof(PatchPrefixes), BindingFlags.Static | BindingFlags.Public) ??
            throw new Exception("Failed to find patch method");
        internal static readonly MethodInfo updatePatchBefore2 =
            typeof(MessageCommandPatch).GetMethod(nameof(PatchPrefixes2), BindingFlags.Static | BindingFlags.Public) ??
            throw new Exception("Failed to find patch method");
        

        //修复玩家消息
        public static void PatchPrefixes(ref string message)
        {
            DoPatch(ref message);
        }
        //修复服务端的消息
        public static void PatchPrefixes2(ref string command)
        {
            DoPatch(ref command);
        }

        private static void DoPatch(ref string str)
        {
            var ctPlugin = CTPlugin.Static;
            
            if (ctPlugin == null)
            {
                return;
            }
            var myKeyValuePairs = ctPlugin.Config.convertors; 
            foreach (var convertor in myKeyValuePairs)
            {
                if (str.Contains(convertor.From))
                {
                    str = str.Replace(convertor.From, convertor.To);
                    return;
                }
            }
        }

        public static void Patch(PatchContext ctx)
        {
            var handleCommandName = nameof(CommandManager.HandleCommand);
            var IsCommandName = nameof(CommandManager.IsCommand);
         
            MethodInfo[] methods =
                typeof(CommandManager).GetMethods(BindingFlags.Instance| BindingFlags.Static| BindingFlags.NonPublic|  BindingFlags.Public );
            foreach (var methodInfo in methods)
            {
                if (methodInfo.Name.Equals(handleCommandName))
                {
                    foreach (var parameterInfo in methodInfo.GetParameters())
                    {
                        if (parameterInfo.Name.Equals("message"))
                        {
                            ctx.GetPattern(methodInfo).Prefixes.Add(updatePatchBefore);
                            break;
                        }
                    }
                }
                if (methodInfo.Name.Equals("HandleCommandFromServerInternal"))
                {
                    foreach (var parameterInfo in methodInfo.GetParameters())
                    {
                        if (parameterInfo.Name.Equals("message"))
                        {
                            ctx.GetPattern(methodInfo).Prefixes.Add(updatePatchBefore);
                            break;
                        }
                    }
                }
                if (methodInfo.Name.Equals(IsCommandName))
                {
                    ctx.GetPattern(methodInfo).Prefixes.Add(updatePatchBefore2);
                }
                
               
            } 

        }


    }
}

