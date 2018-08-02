using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AuthWithDB.ViewModel
{
    public class ContentViewModel
    {
        /// <summary>
        /// Get and Set ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Get and Set the Title of content
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Get and Set the Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Get and Set Content for content
        /// </summary>
        [AllowHtml]
        [Required]
        public string Contents { get; set; }

        /// <summary>
        /// Get and Set Image
        /// </summary>
        [Required]
        public byte[] Image { get; set; }

    }
}