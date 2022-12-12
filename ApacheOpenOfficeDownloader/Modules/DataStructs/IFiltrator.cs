namespace ApacheOpenOfficeDownloader.Modules.DataStructs
{
    /// <summary>
    /// Interface <c>IFiltrator</c> used to check if string satisfy some condition
    /// </summary>
    public interface IFiltrator
    {
        /// <summary>
        /// This method check if <c>input</c> string satisfy some condition 
        /// </summary>
        public bool Match(string input);
    }
}
