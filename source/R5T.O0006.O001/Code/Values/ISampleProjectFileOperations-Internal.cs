using System;

using R5T.L0033.T000;
using R5T.T0131;
using R5T.T0159;
using R5T.T0172.Extensions;


namespace R5T.O0006.O001.Internal
{
    [ValuesMarker]
    public partial interface ISampleProjectFileOperations : IValuesMarker
    {
        public ProjectFileContext PrepareAndGetContext(
            ITextOutput textOutput)
        {
            var projectName = Instances.Values.Sample_ProjectName;
            var projectDirectoryParentDirectoryPath = Instances.DirectoryPaths.Sample_ProjectsDirectoryPath;

            // Use the project directory parent directory path as the project directory path, such that there are no project files in the project's directory.
            // (Just the project file alone should be in the project directory, since we are creating project files.)
            var projectDirectoryPath = projectDirectoryParentDirectoryPath.Value.ToProjectDirectoryPath();

            var projectFilePath = Instances.ProjectPathsOperator.Get_ProjectFilePath(
                projectDirectoryPath,
                projectName);

            // If the project file exists, delete it.
            Instances.FileSystemOperator.DeleteFile_OkIfNotExists(
                projectFilePath.Value);

            // Now create and return the context.
            var projectElement = Instances.ProjectElementOperator.New_ProjectElement();

            var projectContext = new ProjectFileContext
            {
                ProjectName = projectName,
                ProjectFilePath = projectFilePath,
                ProjectElement = projectElement,
                TextOutput = textOutput,
            };

            return projectContext;
        }
    }
}
