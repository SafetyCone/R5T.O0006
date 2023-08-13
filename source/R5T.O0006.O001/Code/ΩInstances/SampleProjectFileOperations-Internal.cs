using System;


namespace R5T.O0006.O001.Internal
{
    public class SampleProjectFileOperations : ISampleProjectFileOperations
    {
        #region Infrastructure

        public static ISampleProjectFileOperations Instance { get; } = new SampleProjectFileOperations();


        private SampleProjectFileOperations()
        {
        }

        #endregion
    }
}
