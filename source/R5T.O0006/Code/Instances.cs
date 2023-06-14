using System;


namespace R5T.O0006
{
    public static class Instances
    {
        public static F0000.IFileOperator FileOperator => F0000.FileOperator.Instance;
        public static F0020.IProjectFileOperator ProjectFileOperator => F0020.ProjectFileOperator.Instance;
        public static F0016.F001.IProjectReferencesOperator ProjectReferencesOperator => F0016.F001.ProjectReferencesOperator.Instance;
        public static L0032.IProjectFileXmlOperator ProjectFileXmlOperator => L0032.ProjectFileXmlOperator.Instance;
        public static L0032.IProjectXmlOperator ProjectXmlOperator => L0032.ProjectXmlOperator.Instance;
    }
}