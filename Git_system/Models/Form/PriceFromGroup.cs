using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web.Configuration;

namespace Git_system.Models.Form
{
    public class PriceFromGroup
    {
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double? USD { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        public double? THD { get; set; }

        public PriceFromGroup()
        {
            // Auto load from config file.
            this.Load();
        }

        /// <summary>
        /// Give PriceFromGroup with data in config file.
        /// </summary>
        /// <returns>PriceFromGroup</returns>
        public PriceFromGroup Load()
        {
            this.THD = getProperty("eComPriceByGroup_THB");
            this.USD = getProperty("eComPriceByGroup_USD");

            return this;
        }

        /// <summary>
        /// Give double form web config field
        /// </summary>
        /// <param name="configField">name of web config</param>
        /// <returns>nullable of double</returns>
        private double? getProperty(string configField)
        {
            try { return Convert.ToDouble(WebConfigurationManager.AppSettings[configField]); }
            catch { return default(double?); }
        }

        /// <summary>
        /// Save PriceFromGroup in configFile
        /// </summary>
        public void Save()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings["eComPriceByGroup_THB"].Value = this.THD.ToString();
            config.AppSettings.Settings["eComPriceByGroup_USD"].Value = this.USD.ToString();
            config.Save();
        }

        /// <summary>
        /// Give PriceFromGroup when save success.
        /// </summary>
        /// <returns>PriceFromGroup with saved</returns>
        public PriceFromGroup SaveMe()
        {
            if (Array.TrueForAll(new double?[] { this.USD, this.THD }, p => p != null))
                this.Save();
            return this;
        }
    }
}