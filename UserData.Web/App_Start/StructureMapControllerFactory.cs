using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using System.Web.Http.Dispatcher;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace UserData.Web.App_Start
{
    public class ServiceActivator : IHttpControllerActivator
    {
        public ServiceActivator(HttpConfiguration configuration) { }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = ObjectFactory.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}