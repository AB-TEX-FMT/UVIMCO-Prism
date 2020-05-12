using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.Shared
{
    /// <summary>
    /// Compnent Meta Data 
    /// <para>ComponentTitle (string) Holds the Title of the Component</para>
    /// <para>ComponentHeader (string) Holds the Header of the Component</para>
    /// <para>ComponentFootNote (string) Holds the FootNote of the Component</para>
    /// </summary>
    public class ComponentMetaData
    {
        /// <summary>
        /// Holds the Components GUID
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Holds the Name of the Component
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds the Description of the Component
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Holds the Components TypeID
        /// </summary>
        public int ComponentTypeID { get; set; }

        /// <summary>
        /// Holds the Components Type
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// Holds the Title of the Component
        /// </summary>
        public string ComponentTitle { get; set; }

        /// <summary>
        /// Holds the Header of the Component
        /// </summary>
        public string ComponentHeader { get; set; }

        /// <summary>
        /// Holds the Footnote of the Component
        /// </summary>
        public string ComponentFootNote { get; set; }


    }
}
