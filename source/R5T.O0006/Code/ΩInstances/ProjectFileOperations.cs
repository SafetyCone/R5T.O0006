using System;


namespace R5T.O0006
{
    public class ProjectFileOperations : IProjectFileOperations
    {
        #region Infrastructure

        public static IProjectFileOperations Instance { get; } = new ProjectFileOperations();


        private ProjectFileOperations()
        {
        }

        #endregion
    }
}
