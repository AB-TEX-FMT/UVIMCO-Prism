using System;
using DataModel.BaseModels;
using DataModel.Shared;

namespace DataModel.DTOModels
{
    /// <summary>
    /// Base Data Transfer Object Model
    /// <para>Opts (SearchOptions) Holds incoming/outgoing search parms</para>
    /// </summary>
    [Serializable()]
    public class BaseDTOModel : BaseModel
    {
        /// <summary>
        /// parameterless default constructor only for serialization
        /// </summary>
        public BaseDTOModel() { }

        /// <summary>
        /// Holds incoming/outgoing search parms
        /// </summary>
        public SearchOptions Opts { get; set; }
    }
}