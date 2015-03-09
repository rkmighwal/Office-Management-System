using System.Linq.Expressions;
namespace System.Web.Mvc
{
    public static partial class HtmlHelperExtensions
    {

        /// <summary>
        /// Returns an Html String represent Boolean Value.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="value">The Value of Field.</param>
        /// <returns>An HTML String</returns>
        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool? value)
        {
            var text = value.HasValue && value.Value ? "Yes" : "No";
            return new MvcHtmlString(text);
        }

        /// <summary>
        /// Returns an Html String represent Boolean Value.
        /// </summary>
        /// <typeparam name="TModel">The model object.</typeparam>
        /// <typeparam name="TValue">The property value of model.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">Linq Expression represent Model property.</param>
        /// <returns>An HTML String</returns>
        public static MvcHtmlString YesNoFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return YesNo(htmlHelper, (Convert.ToBoolean(metadata.Model)));
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">HTML helper instance that this method extends.</param>
        /// <param name="linkText">Inner text of the anchor element.</param>
        /// <param name="action">[Optional] Name of the action.</param>
        /// <param name="controller">[Optional] Name of the controller.</param>
        /// <param name="iconClass">[Optional] CSS class of Font-Awesome Icon.</param>
        /// <param name="htmlAttributes">[Optional] An object that contains the HTML attributes for the element. The attributes
        /// are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.</param>
        /// <returns>An anchor element (a element).</returns>
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string action = "", string controller = "", string iconClass = "", object htmlAttributes = null)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            if (string.IsNullOrEmpty(linkText))
                throw new ArgumentNullException("linkText");

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(iconClass))
                iconClass = string.Empty;

            var anchorString = "<a ";

            if (htmlAttributes != null)
            {
                var type = htmlAttributes.GetType();

                foreach (var property in type.GetProperties())
                {
                    if (property.Name.StartsWith("data"))
                    {
                        // Adding Data Attribute in tag
                        anchorString += " " + property.Name.Replace('_', '-');

                        // Adding Data Attribute Value in tag
                        var propertyValue = property.GetValue(htmlAttributes).ToString();
                        if (!string.IsNullOrEmpty(propertyValue))
                            anchorString += "=\"" + propertyValue + "\"";
                    }
                    else if (property.Name.ToLower() == "class")
                    {
                        // Adding Class Attribute with value in tag
                        anchorString += " class=\"" + property.GetValue(htmlAttributes) + "\"";
                    }
                }
            }
            anchorString += " id=\"nav" + linkText.ToString() + "\" href=\"/" + controller + "/" + action + "\">";
            if (!string.IsNullOrEmpty(iconClass))
                anchorString += "<i class=\"" + iconClass + "\"></i><span>" + linkText + "</span>";
            else
                anchorString += linkText;
            anchorString += "</a>";
            return new MvcHtmlString(anchorString);
        }

        /// <summary>
        /// Returns a list item (li) element that contains the virtual path of the
        /// specified action.
        /// 
        /// Exceptions:
        /// ArgumentNullException
        /// </summary>
        /// <param name="htmlHelper">HTML helper instance that this method extends.</param>
        /// <param name="linkText">Inner text of the anchor element.</param>
        /// <param name="actionName">[Optional] Name of the action.</param>
        /// <param name="controllerName">[Optional] Name of the controller.</param>
        /// <param name="iconClass">[Optional] CSS class of Font-Awesome Icon.</param>
        /// <param name="htmlAttributes">[Optional] An object that contains the HTML attributes for the element. The attributes
        /// are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.</param>
        /// <returns>A list item (li) element.</returns>
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string text, string action = "", string controller = "", string iconClass = "", object htmlAttributes = null)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(iconClass))
                iconClass = string.Empty;

            var li = new TagBuilder("li");
            if (string.Equals(currentAction,
                              action,
                              StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController,
                              controller,
                              StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text,
                                            action,
                                            controller,
                                            iconClass).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="iconClass">CSS class of Font-Awesome Icon.</param>
        /// <param name="action">[Optional] Name of the action.</param>
        /// <param name="controller">[Optional] Name of the controller.</param>
        /// <param name="addButtonText">[Optional] Add Title as Button Text. Default: false.</param>
        /// <param name="title">[Optional] The title of anchor tag.
        /// [Required] Id "addButtonText" is set to true.</param>
        /// <param name="htmlAttributes">[Optional] An object that contains the HTML attributes for the element. The attributes
        /// are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.</param>
        /// <param name="objectValues">[Optional] An object that contains the parameters for a route. The parameters are retrieved
        /// through reflection by examining the properties of the object. The object
        /// is typically created by using object initializer syntax.</param>
        /// <returns>An anchor element (a element).</returns>
        public static MvcHtmlString IconLink(this HtmlHelper htmlHelper, string iconClass, string action = "", string controller = "", bool addButtonText = false, string title = "", object htmlAttributes = null, object objectValues = null)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");
            
            if (string.IsNullOrEmpty(action))
                action = currentAction;

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (addButtonText && string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title", "title can't be empty or null, if addButtonText is set to true.");

            var anchorString = "<a ";
            if (htmlAttributes != null)
            {
                var type = htmlAttributes.GetType();

                foreach (var property in type.GetProperties())
                {
                    if (property.Name.StartsWith("data"))
                    {
                        // Adding Data Attribute in tag
                        anchorString += " " + property.Name.Replace('_', '-');

                        // Adding Data Attribute Value in tag
                        var propertyValue = property.GetValue(htmlAttributes).ToString();
                        if (!string.IsNullOrEmpty(propertyValue))
                            anchorString += "=\"" + propertyValue + "\"";
                    }
                    else if (property.Name.ToLower() == "class")
                    {
                        // Adding Class Attribute with value in tag
                        anchorString += " class=\"" + property.GetValue(htmlAttributes) + "\"";
                    }
                }
            }
            anchorString += " title=\"" + (!string.IsNullOrEmpty(title) ? title : string.Empty) + "\"";
            anchorString += " href=\"/" + (!string.IsNullOrEmpty(controller) ? controller : currentController) + "/" + action;
            if (objectValues != null)
            {
                var type = objectValues.GetType();
                if (type.GetProperty("id") != null)
                    anchorString += "/" + type.GetProperty("id").GetValue(objectValues);
            }
            anchorString += "\"><i class=\"fa " + iconClass + "\"></i>" + (addButtonText ? "<span> " + title + "</span>" : "") + "</a>";
            return new MvcHtmlString(anchorString);
        }

    }
}