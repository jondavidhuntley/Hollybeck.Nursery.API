using System;

namespace Hollybeck.Domain.Entities
{
    public class ImageDetail
    {
        public ImageDetail()
        { }

        /// <summary>
        /// Gets or sets the Plant Image
        /// </summary>
        public string Base64Image { get; set; }       

        /// <summary>
        /// Gets or sets the Plant Image Filename
        /// </summary>
        public string ImageFilename { get; set; }

        /// <summary>
        /// Gets or sets the Plant Last Updated Date
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the User who last changed the Plant
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}