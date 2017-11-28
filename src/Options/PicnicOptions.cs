using System;

namespace Picnic.Options
{
    /// <summary>
    /// Options to change the defaul behavior of Picnic
    /// </summary>
    public class PicnicOptions
    {
        /// <summary>
        /// Gets or sets the DynamicView options
        /// </summary>
        public DynamicViewOptions DynamicView { get; set; }

        /// <summary>
        /// Gets or sets the Manage options
        /// </summary>
        public ManageOptions Manage { get; set; }

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public PicnicOptions()
        {
            this.DynamicView = DynamicViewOptions.Default;
            this.Manage = ManageOptions.Default;            
        }

        /// <summary>
        /// Specifies default options to be used if none are specified
        /// </summary>
        public static Action<PicnicOptions> Default = options =>
        {
            options.DynamicView = DynamicViewOptions.Default;
            options.Manage = ManageOptions.Default;
        };        
    }
}