﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //web api deki startup karşılığı
            //.net'e ben senin ıoc yapını değil AutofacBusinessModule yapımı kullanacağımı belirtmen lazım
            //WebAPI---->Program.cs
            //birisi senden ICarService isterse CarManager instance'ı ver
            //SingleInstance : Tek bir instance oluşturur
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<EFCarDAL>().As<ICarDAL>().SingleInstance();

            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EFBrandDAL>().As<IBrandDAL>().SingleInstance();

            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<EFRentalDAL>().As<IRentalDAL>().SingleInstance();

            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EFColorDAL>().As<IColorDAL>().SingleInstance();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EFCustomerDAL>().As<ICustomerDAL>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EFUserDAL>().As<IUserDAL>().SingleInstance();

            builder.RegisterType<CarImageManager>().As<ICarImageService>().SingleInstance();
            builder.RegisterType<EFCarImageDAL>().As<ICarImageDAL>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JWTHelper>().As<ITokenHelper>();

            builder.RegisterType<CreditCardManager>().As<ICreditCardService>();
            builder.RegisterType<EFCreditCardDAL>().As<ICreditCardDAL>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();



            //InstancePerHttpRequest--------->Her ayrı web isteği için hizmetimizin tek bir örneğini alır 
            //InstancePerApiRequest--------->Her ayrı web isteği için hizmetimizin tek bir örneğini alır 
            //InstancePerLifetimeScope
            //SingleInstance
            //.......

        }
    }
}