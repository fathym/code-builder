using System.Text.Json.Nodes;

namespace Fathym.LCU.GrapesJS
{
    public class GrapesJSConfig
    {
        #region Properties
        public virtual string? Container { get; set; }

        public virtual bool FromElement { get; set; }

        public virtual string? Height { get; set; }

        public virtual int NoticeOnUnload { get; set; }

        public virtual GrapesJSPanels? Panels { get; set; }

        public virtual List<string> Plugins { get; set; }

        public virtual JsonObject PluginsOpts { get; set; }

        public virtual GrapesJSStorageManager? StorageManager { get; set; }

        public virtual string? Width { get; set; }
        #endregion

        #region Constructors
        public GrapesJSConfig()
        {
            FromElement = true;

            Height = "100%";

            NoticeOnUnload = 0;

            //Panels = new GrapesJSPanels();

            Plugins = new List<string>() { "gjs-blocks-basic" };

            PluginsOpts = new JsonObject()
            {
                ["gjs-blocks-basic"] = new JsonObject()
            };

            StorageManager = new GrapesJSStorageManager()
            {
                Autoload = 0
            };

            Width = "auto";
        }
        #endregion
    }

    public class GrapesJSStorageManager
    {
        public virtual int Autoload { get; set; }

        //[JsonExtensionData]
        //public virtual JsonObject Metadata { get; set; }
    }

    public class GrapesJSPanels
    {
        #region Properties
        public virtual List<JsonObject>? Defaults { get; set; }
        #endregion

        #region Constructors
        public GrapesJSPanels()
        {
            Defaults = new List<JsonObject>();
        }
        #endregion
    }
}