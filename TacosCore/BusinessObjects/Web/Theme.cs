//-----------------------------------------------------------------------
// <copyright file="Theme.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Globalization;

namespace TacosCore.BusinessObjects.Web
{
    public class Theme
    {
        const string BsNativeDarkModePostfix = "-dark";

        public Theme(string name, bool isBootstrapNative)
        {
            Name = name;
            IsBootstrapNative = isBootstrapNative;
        }

        public string GetCssClass(bool isActive) => isActive ? "active" : "text-body";

        public string BootstrapThemeMode => IsBootstrapNative && Name.Contains(BsNativeDarkModePostfix) ? "dark" : "light";
        public string IconCssClass { get { return Name.ToLower(); } }
        public bool IsBootstrapNative { get; set; }
        public string Name { get; set; }
        public string ThemePath => IsBootstrapNative ? Name.Replace(BsNativeDarkModePostfix, string.Empty) : Name;
        public string Title { get { return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Name.Replace("-", " ")); } }
    }

    public class ThemeSet
    {
        static readonly HashSet<string> BuiltInThemes = new() {
            "blazing-berry", "blazing-dark", "purple", "office-white", "fluent-light", "fluent-dark"
        };

        public ThemeSet(string title, params string[] themes)
        {
            Title = title;
            Themes = themes.Select(CreateTheme).ToArray();


            Theme CreateTheme(string name)
            {
                bool isBootstrapNative = !BuiltInThemes.Contains(name);
                return new Theme(name, isBootstrapNative);
            }
        }

        public Theme[] Themes { get; set; }
        public string Title { get; set; }
    }

}
