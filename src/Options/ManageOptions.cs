using System;
using ctorx.Core.Mvc.Messaging.Options;

namespace Picnic.Options
{
    /// <summary>
    /// Options specific to the Picnic Management interface
    /// </summary>
    public class ManageOptions
    {
        /// <summary>
        /// Gets or sets the Layout
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// Gets or sets the EditorOptions
        /// </summary>
        public EditorOptions EditorOptions { get; set; }

        /// <summary>
        /// Gets the default manage options 
        /// </summary>
        public static ManageOptions Default => new ManageOptions
        {
            Layout = null,
            EditorOptions = EditorOptions.Default
        };
    }
}