using System;
using System.Threading.Tasks;

using R5T.L0033.T000;
using R5T.T0131;
using R5T.T0159;


namespace R5T.O0006.O001
{
    [ValuesMarker]
    public partial interface ISampleProjectFileOperations : IValuesMarker
    {
        private static Internal.ISampleProjectFileOperations Internal => O001.Internal.SampleProjectFileOperations.Instance;


        public Task In_New_SampleProjectFileContext(
            ITextOutput textOutput,
            Func<IProjectFileContext, Task> projectContextAction = default)
        {
            var projectContext = Internal.PrepareAndGetContext(textOutput);

            return Instances.ProjectFileContextOperator.In_New_ProjectFileContext(
                projectContext,
                projectContextAction);
        }

        public void In_New_SampleProjectFileContext(
            ITextOutput textOutput,
            Action<IProjectFileContext> projectContextAction = default)
        {
            var projectContext = Internal.PrepareAndGetContext(textOutput);

            Instances.ProjectFileContextOperator.In_New_ProjectFileContext(
                projectContext,
                projectContextAction);
        }
    }
}
