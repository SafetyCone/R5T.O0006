using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0131;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0179.Extensions;
using R5T.T0181;
using R5T.T0208;


namespace R5T.O0006
{
    [ValuesMarker]
    public partial interface IProjectFileOperations : IValuesMarker
    {
        public async Task<PackageReference[]> Get_PackageReferences(IProjectFilePath projectFilePath)
        {
            var packageReferences = await Instances.ProjectFileXmlOperator.Query_ProjectElementContext(
                projectFilePath,
                Instances.ProjectXmlOperator.Get_PackageReferences);

            return packageReferences;
        }

        public async Task<IProjectFilePath[]> Get_TopLevelProjectReferences(
            IEnumerable<IProjectFilePath> projectFilePaths)
        {
            var values = await Instances.ProjectReferencesOperator.Get_TopLevelProjectReferences(
                projectFilePaths.Get_Values())
                ;

            var output = values.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences(
            IProjectFilePath projectFilePath)
        {
            var recursiveProjectReferences = await Instances.ProjectReferencesOperator.GetAllRecursiveProjectReferences(
                projectFilePath.Value);

            var output = recursiveProjectReferences.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        /// <inheritdoc cref="F0016.F001.IProjectReferencesOperator.Get_RecursiveProjectReferences_InDependencyOrder(IEnumerable{string})"/>
        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences_InDependencyOrder(
            IProjectFilePath projectFilePath)
        {
            var recursiveProjectReferences = await Instances.ProjectReferencesOperator.Get_RecursiveProjectReferences_InDependencyOrder(
                projectFilePath.Value);

            var output = recursiveProjectReferences.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        public async Task Write_RecursiveProjectReferencesTo(
            IProjectFilePath projectFilePath,
            ITextFilePath textFilePath)
        {
            var recursiveProjectReferences = await this.Get_RecursiveProjectReferences(projectFilePath);

            await Instances.FileOperator.WriteLines(
                textFilePath.Value,
                recursiveProjectReferences.Get_Values().OrderAlphabetically());
        }

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
