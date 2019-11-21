namespace DataModel.BaseModels
{
    /// <summary>
    /// Base Model
    /// <para>HasError (bool) Holds Error Flag</para>
    /// <para>ErrorMessage (string) Holds the Error Message</para>
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// True/False that determines whether there is an error message
        /// </summary>
        public virtual bool HasError { get; set; }
        /// <summary>
        /// Holds the error message if HasError = True
        /// </summary>
        public virtual string ErrorMessage { get; set; }
    }
}