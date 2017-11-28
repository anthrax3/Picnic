using ctorx.Core.Mvc.Messaging;

namespace Picnic.Areas
{
    /// <summary>
    /// Specifies default messages used by the Picnic admin interface
    /// </summary>
    internal class PicnicDefaultMessages : IDefaultMessages
    {
        public string DefaultSuccessMessage => "Your changes have been saved";
        public string DefaultErrorMessage => "Uh oh, something went wrong.  Please try again";
    }
}