using System;
using System.Collections.Generic;

namespace Picnic.Options
{
    public class EditorOptions
    {
        /// <summary>
        /// Gets the Plugins
        /// </summary>
        public IEnumerable<string> Plugins { get; private set; }

        /// <summary>
        /// Gets the Toolbar Items
        /// </summary>
        public IEnumerable<string> Buttons { get; private set; }

        /// <summary>
        /// Gets the Stylesheets
        /// </summary>
        public IEnumerable<string> Stylesheets { get; private set; }
        
        /// <summary>
        /// Gets or sets the EditorBaseUrl
        /// </summary>
        public string EditorBaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the minumum height, in pixels, of the editor
        /// </summary>
        public int MinHeight { get; set; }

        /// <summary>
        /// Adds one or more plugins to the editor
        /// </summary>
        /// <param name="plugins">List of plugins to be loaded with the editor</param>
        public void SetPlugins(params string[] plugins)
        {
            this.Plugins = plugins ?? new List<string>().ToArray();
        }

        /// <summary>
        /// Adds one or more buttons to the editor
        /// </summary>
        /// <param name="buttons">List of buttons to be added to the editor</param>
        public void SetButtons(params string[] buttons)
        {
            this.Buttons = buttons ?? new List<string>().ToArray();
        }

        /// <summary>
        /// Adds one or more buttons to the editor
        /// </summary>
        /// <param name="stylesheets">List of stylesheet paths to apply to the editor</param>
        public void SetStylesheets(params string[] stylesheets)
        {
            this.Stylesheets = stylesheets ?? new List<string>().ToArray();
        }

        /// <summary>
        /// Gets the default editor options
        /// </summary>
        public static EditorOptions Default => new EditorOptions
        {
            Plugins = new List<string> { "code", "searchreplace", "fullscreen", "image", "link", "media", "table", "charmap", "hr", "nonbreaking", "anchor", "advlist", "lists", "textcolor", "contextmenu", "colorpicker", "help" },
            Buttons = new List<string> { "insertfile", "formatselect", "|", "bold", "italic", "strikethrough", "forecolor", "backcolor", "|", "alignleft", "aligncenter", "alignright", "alignjustify", "|", "anchor", "link", "image", "media", "hr", "table", "|", "numlist", "bullist", "outdent", "indent", "|", "removeformat", "|", "code", "fullscreen" },            
            Stylesheets = new List<string>(),
            MinHeight = 200
        };
    }
}