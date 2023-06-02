using System;

using R5T.T0131;
using R5T.T0172;



namespace R5T.O0006
{
    [ValuesMarker]
    public partial interface IProjectFileOperations : IValuesMarker
    {
        /// <inheritdoc cref="F0020.IProjectFileOperator.IsProjectFile(string)"/>
        public bool Is_ProjectFile(
            IProjectFilePath projectFilePath)
        {
            var output = Instances.ProjectFileOperator.IsProjectFile(
                projectFilePath.Value);

            return output;
        }
    }
}
