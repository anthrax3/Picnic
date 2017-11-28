using Picnic.Controllers;

namespace Picnic.Options
{
    /// <summary>
    /// Options specific to the dynamic view for Picnic pages
    /// </summary>
    public class DynamicViewOptions
    {
        /// <summary>
        /// Gets or sets the Name of the Action used to render the page
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the Layout used by the dynamic view
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// Gets the default dynamic view options if none are specified
        /// </summary>
        public static DynamicViewOptions Default => new DynamicViewOptions
        {
            Name = nameof(RenderController.DynamicPage),
            Layout = null
        };
    }
}