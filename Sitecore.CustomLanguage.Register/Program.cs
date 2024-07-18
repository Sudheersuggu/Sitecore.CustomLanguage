using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sitecore.CustomLanguage.Register
{
    public class Program
    {
        /// <summary>
        /// Console application to register custom languages in windows
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            System.Console.WriteLine("Creating new Custom Language for Sitecore and press enter to contine.... ");

            // Create our own list of the languages that we want to register
            List<CustomCulture> customCultures = new List<CustomCulture>()
            {
                //new CustomCulture("BaseLanguage", "RegionName", "Language Culture", "Language Tag", "Language Name", "Language Native Name", "Regional Language Name", "Regional Native Language Name"),
                new CustomCulture("en-GB", "GB", "en-150", "en-150", "English (Europe)", "English (Europe)", "Europe", "Europe"),
                new CustomCulture("en-GB", "GB", "en-IN", "en-IN", "English (India)", "English (India)", "India", "India"),
                new CustomCulture("en-GB", "GB", "en-SA", "en-SA", "English (Saudi Arabia)", "English (Saudi  Arabia)", "Saudi Arabia", "Saudi Arabia"),
                new CustomCulture("en-US", "CO", "en-CO", "en-CO", "English (Colombia)", "English (Colombia)", "Colombia", "Colombia")
        };

            CultureAndRegionInfoBuilder cib = null;
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            try
            {
                // Loop through the list of custom cultures and register them and if language already exists then skip
                foreach (var culture in customCultures)
                {
                    if (!allCultures.Any(cul => string.Equals(cul.Name, culture.CultureName, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        cib = new CultureAndRegionInfoBuilder(culture.CultureName, CultureAndRegionModifiers.None);
                        cib.LoadDataFromCultureInfo(new CultureInfo(culture.BaseFrom));
                        cib.LoadDataFromRegionInfo(new RegionInfo(culture.BaseFromReg));
                        cib.CultureEnglishName = culture.EnglishName;
                        cib.CultureNativeName = culture.NativeName;
                        cib.IetfLanguageTag = culture.CultureLangTag;
                        cib.RegionEnglishName = culture.RegEnglishName;
                        cib.RegionNativeName = culture.RegNativeName;
                        cib.Register();
                        System.Console.WriteLine(cib.CultureName + " => created");
                    }
                    else
                    {
                        System.Console.WriteLine(culture.CultureName + " => Already exists");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            System.Console.WriteLine("Successfully registed the required languages.Press any key to exit");
        }

        /// <summary>
        /// Custom Culture class to hold the custom culture information
        /// </summary>
        class CustomCulture
        {
            public string BaseFrom { get; set; }
            public string BaseFromReg { get; set; }
            public string CultureName { get; set; }
            public string CultureLangTag { get; set; }
            public string EnglishName { get; set; }
            public string NativeName { get; set; }
            public string RegEnglishName { get; set; }
            public string RegNativeName { get; set; }

            /// <summary>
            /// Constructor to initialize the custom culture
            /// </summary>
            /// <param name="baseFrom">Base Language from where the culture need to be copied</param>
            /// <param name="baseFromReg">Base region</param>
            /// <param name="cultureName">Language Culture</param>
            /// <param name="cultureLangTag">Language Culture Tag</param>
            /// <param name="englishName">Language Name</param>
            /// <param name="nativeName">Language Native Name</param>
            /// <param name="regEnglishName">Regional Language Name</param>
            /// <param name="regNativeName">Regional Native Language Name</param>
            public CustomCulture(string baseFrom, string baseFromReg, string cultureName, string cultureLangTag, string englishName, string nativeName, string regEnglishName, string regNativeName)
            {
                this.BaseFrom = baseFrom;
                this.BaseFromReg = baseFromReg;
                this.CultureName = cultureName;
                this.CultureLangTag = cultureLangTag;
                this.EnglishName = englishName;
                this.NativeName = nativeName;
                this.RegEnglishName = regEnglishName;
                this.RegNativeName = regNativeName;
            }
        }
    }
}
