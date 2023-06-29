using System;
using System.Threading.Tasks;

using R5T.T0141;
using R5T.T0172.Extensions;


namespace R5T.O0006.Construction
{
    [DemonstrationsMarker]
    public partial interface IDemonstrations : IDemonstrationsMarker
    {
        /// <summary>
        /// Given a project file instance, identify and remedy variances and write the result to a separate output file.
        /// Allows comparison of (possible) modified and unmodified project files in Notepad++.
        /// </summary>
        public async Task StandardizeVariances()
        {
            /// Inputs.
            var inputProjectFilePath =
                @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.O0006\source\R5T.O0006.Construction\R5T.O0006.Construction.csproj"
                .ToProjectFilePath()
                ;
            var outputProjectFilePath =
                Instances.Paths.Sample_ProjectFilePath
                ;


            /// Run.
            await Instances.ProjectFileOperations.IdentifyAndRemedyVariances(
                inputProjectFilePath,
                outputProjectFilePath);

            Instances.NotepadPlusPlusOperator.Open(
                inputProjectFilePath.Value,
                outputProjectFilePath.Value);
        }
    }
}
