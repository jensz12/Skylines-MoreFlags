﻿using System;
using System.Linq;
using ICities;
using MoreFlags.OptionsFramework;

namespace MoreFlags
{
    public class Mod : IUserMod
    {

        public string Name
        {
            get
            {
                OptionsWrapper<Options>.Ensure();
                return "More Flags";
            }
        }

        public string Description => "More Flags";

        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("MoreFlags");
            var defaultIndex = 0;
            if (OptionsWrapper<Options>.Options.replacement != string.Empty)
            {
                for (var i = 0; i < LoadingExtension.Flags.Count; i++)
                {
                    var flag = LoadingExtension.Flags[i];
                    if (!flag.id.Equals(OptionsWrapper<Options>.Options.replacement))
                    {
                        continue;
                    }
                    defaultIndex = i + 1;
                    break;
                }
                if (defaultIndex == 0)
                {
                    OptionsWrapper<Options>.Options.replacement = string.Empty;
                    OptionsWrapper<Options>.SaveOptions();
                }
            }

            group.AddDropdown("Replace stock Flags with",
                new[] { "-----" }.Concat(LoadingExtension.Flags.Select(flag => flag.description)).ToArray(), defaultIndex, sel =>
                 {
                     OptionsWrapper<Options>.Options.replacement = sel == 0 ? string.Empty : LoadingExtension.Flags[sel - 1].id;
                     OptionsWrapper<Options>.SaveOptions();
                 });
        }
    }
}
