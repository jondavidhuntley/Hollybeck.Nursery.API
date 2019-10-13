using System;

namespace Hollybeck.Domain.Entities
{
    public class Plant
    {
        public Plant()
        { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Plant Soil
        /// </summary>
        public string Soil { get; set; }

        /// <summary>
        /// Gets or sets the Plant Size
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the Plant Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the Plant Foliage
        /// </summary>
        public string Foliage { get; set; }

        /// <summary>
        /// Gets or sets the Unit Price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the Price Info
        /// </summary>
        public string PriceInfo { get; set; }

        /// <summary>
        /// Gets or sets the Plant Image
        /// </summary>
        public string Base64Image { get; set; }

        /// <summary>
        /// Gets or sets the Plant Image File
        /// </summary>
        public byte[] BinaryImageFile { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether the Plant is Plant of the Month
        /// </summary>
        public bool Current { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Plant is New
        /// </summary>
        public bool NewIn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Plant is an RHS Pollinator
        /// </summary>
        public bool RhsPollinator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Plant has the RHS AGM Award
        /// </summary>
        public bool RhsAgmAward { get; set; }

        /// <summary>
        /// Gets or sets the Plant Seasonal Start Date
        /// </summary>
        public DateTime SeasonalStart { get; set; }

        /// <summary>
        /// Gets or sets the Plant Seasonal End Date
        /// </summary>
        public DateTime SeasonalEnd { get; set; }

        /// <summary>
        /// Gets a value indicating whether the Plant is currently in Season
        /// </summary>
        public bool InSeason
        {
            get
            {
                if (DateTime.Now > SeasonalStart && DateTime.Now < SeasonalEnd)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a Short Description
        /// </summary>
        public string ShortDescription
        {
            get
            {
                if (Description.Length > 400)
                {
                    return String.Format("{0}...", Description.Substring(0, 400));
                }
                else
                {
                    return Description;
                }
            }
        }
    }
}