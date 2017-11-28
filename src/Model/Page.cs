using System;
using System.ComponentModel.DataAnnotations;

namespace Picnic.Model
{
    /// <summary>
    /// Represents a page that gets rendered dynamically
    /// </summary>
    public class Page : IPicnicEntity
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name must be 50 characters or less")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        [Required(ErrorMessage = "Path is required")]
        [RegularExpression(@"^/{1}([\w\-]|/)*$", ErrorMessage = "Path must be in the format of \"/your-path\"")]
        [MaxLength(1500, ErrorMessage = "Path must be 1,500 characters or less")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Canonical
        /// </summary>
        public string Canonical { get; set; }

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        [Required(ErrorMessage = "required")]
        public string Content { get; set; }

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