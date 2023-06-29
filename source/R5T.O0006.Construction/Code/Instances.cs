using System;


namespace R5T.O0006.Construction
{
    public static class Instances
    {
        public static Z0046.IFilePaths Paths => Z0046.FilePaths.Instance;
        public static F0033.INotepadPlusPlusOperator NotepadPlusPlusOperator => F0033.NotepadPlusPlusOperator.Instance;
        public static IProjectFileOperations ProjectFileOperations => O0006.ProjectFileOperations.Instance;
    }
}