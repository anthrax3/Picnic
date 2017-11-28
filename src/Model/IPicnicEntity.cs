using System;

namespace Picnic.Model
{
    /// <summary>
    /// Represents an entity used in Picnic
    /// </summary>
    public interface IPicnicEntity
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the item
        /// </summary>
        string LastModifyUser { get; set; }

        /// <summary>
        /// Gets or sets the date the item was last modified
        /// </summary>
        DateTime LastModifyDate { get; set; }
    }
}