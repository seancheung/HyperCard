﻿using System.Collections.Generic;

namespace MODEL
{
    /// <summary>
    /// A format is a list of legal sets and banned cards
    /// </summary>
    public class Format
    {
        /// <summary>
        /// Name of the format
        /// </summary>
        public FORMAT FormatName
        {
            get;
            set;
        }

        /// <summary>
        /// A list that contains names of all legal sets
        /// </summary>
        public List<string> LegalSets
        {
            get;
            set;
        }
        /// <summary>
        /// A list that contains names of all banned cards
        /// </summary>
        public List<string> BannedCards
        {
            get;
            set;
        }

        /// <summary>
        /// Initializing
        /// </summary>
        public Format()
        {
            LegalSets = new List<string>();
            BannedCards = new List<string>();
        }

        /// <summary>
        /// Initialize with parameters
        /// </summary>
        /// <param name="format">Format name</param>
        /// <param name="sets">Legal sets</param>
        /// <param name="cards">Banned cards</param>
        public Format(FORMAT format, List<string> sets, List<string> cards)
        {
            LegalSets = new List<string>();
            BannedCards = new List<string>();
            FormatName = format;
            LegalSets = sets;
            BannedCards = cards;
        }
    }
}
