using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Picnic.Model
{
    /// <summary>
    /// Represents a piece of content
    /// </summary>
    public class Content : IPicnicEntity
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Key
        /// </summary>
        [Required(ErrorMessage = "Key is required")]
        [RegularExpression(@"^[\w\-_]+$", ErrorMessage = "Key may only contain letters, numbers, dashes and underscores")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name must be 50 characters or less")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        [Required(ErrorMessage = "required")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the IsActive
        /// </summary>        
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the item
        /// </summary>
        public string LastModifyUser { get; set; }

        /// <summary>
        /// Gets or sets the date the item was last modified
        /// </summary>
        public DateTime LastModifyDate { get; set; }
    }
}