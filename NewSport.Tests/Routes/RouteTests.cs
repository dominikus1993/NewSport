using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewSport.WebApi;

namespace NewSport.Tests.Routes
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl=null, string httpMethod="GET")
        {
            Mock<HttpRequestBase> mockReguest = new Mock<HttpRequestBase>();
            mockReguest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockReguest.Setup(m => m.HttpMethod).Returns(httpMethod);
            
            Mock<HttpResponseBase> mockBase = new Mock<HttpResponseBase>();
            mockBase.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockReguest.Object);
            mockContext.Setup(m => m.Response).Returns(mockBase.Object);

            return mockContext.Object;
        }

        private bool TestIncomingRouteResult(RouteData routeData, string controller, string action,
            object propertySet = null)
        {
            Func<object, object, bool> compare = (x, y) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(x, y) == 0;
            };
            bool result = compare(routeData.Values["controller"], controller) &&
                          compare(routeData.Values["action"], action);
            if (propertySet != null)
            {
                PropertyInfo[] propertyInfos = propertySet.GetType().GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    if (
                        !(routeData.Values.ContainsKey(propertyInfo.Name) &&
                          compare(routeData.Values[propertyInfo.Name], propertyInfo.GetValue(propertySet, null))))
                    {
                        return false;
                    }
                }
            }
            return result;
        }

        private void TestRouteMatch(string url,string controller,string action, object routeProperties = null, string httpMethod="Get")
        {
            RouteCollection routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(CreateHttpContext(url,httpMethod));

            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result,controller,action,routeProperties));
        }
        [TestMethod]
        public void TestRouteMatch()
        {
           TestRouteMatch("~/","Post","Index");
           TestRouteMatch("~/Post/Get/1","Post","Get",new {id="1"});
           TestRouteMatch("~/Post/Delete/1","Post","Delete",new{id="1"});
           TestRouteMatch("~/Post/Add", "Post", "Add");
           TestRouteMatch("~/Post/Edit/1", "Post", "Edit", new { id = "1" });
           TestRouteMatch("~/Account/Login", "Account", "Login");
           TestRouteMatch("~/Account/Register", "Account", "Register");
        }
    }
}
