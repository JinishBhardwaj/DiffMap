using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace DiffService.Helpers.UI
{
    /// <summary>
    /// The <see cref="ByteArrayModelBinder"/> provides a mechanism to
    /// bind a  byte[] to the web api endpoint as a provided post parameter Model
    /// </summary>
    public class ByteArrayModelBinder : IModelBinder
    {
        #region IModelBinder members

        /// <summary>
        /// Gets the provided model and sets it as the model of the endpoint
        /// </summary>
        /// <param name="actionContext">Http Action information</param>
        /// <param name="bindingContext">Model binding information</param>
        /// <returns></returns>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(byte[])) return false;
            var content = actionContext.Request.Content.ReadAsByteArrayAsync().Result;
            if (content != null && content.Length > 0)
            {
                bindingContext.Model = content;
                return true;
            }

            return false;
        }

        #endregion
    }
}