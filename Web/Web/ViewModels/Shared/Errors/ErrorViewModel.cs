namespace Web.ViewModels.Shared.Errors
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string Message { get; set; }

        public bool ShowRequestId => ShowId();

        private bool ShowId()
        {
            return
        #if RELEASE
            true;
        #else
            false;
        #endif
        }


    }
}