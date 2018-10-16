using Ninject;
using System;
using System.Linq;
using TRIINKOM.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure
{
    public class ApplicationController : IAppController
    {
        private readonly IKernel _container = new StandardKernel();

        public IAppController RegisterConstant<TConstant>(TConstant constant)
        {
            _container.Rebind<TConstant>().ToConstant(constant);
            return this;
        }

        public IAppController Register(Type type, Type implementation)
        {
            _container.Rebind(type).To(implementation);
            return this;
        }

        public IAppController Register<TService, TImpl>() where TImpl : TService
        {
            _container.Rebind<TService>().To<TImpl>();
            return this;
        }

        public IAppController Register<T>(Func<T> factoryMethod)
        {
            _container.Rebind<T>().ToMethod(ctx => factoryMethod());
            return this;
        }

        public T Get<T>(params IParameter[] args)
        {
            if (args.Length == 0)
                return _container.Get<T>();

            return _container.Get<T>(args.Select(arg =>
                new Ninject.Parameters.ConstructorArgument(arg.Name, arg.Value)).ToArray());
        }

        public TResult Get<TResult>(Type type, params IParameter[] args)
        {
            if (args.Length == 0)
                return (TResult)_container.Get(type);

            return (TResult)_container.Get(type, args.Select(arg =>
                new Ninject.Parameters.ConstructorArgument(arg.Name, arg.Value)).ToArray());
        }

        public object Get(Type type, params IParameter[] args)
        {
            if (args.Length == 0)
                return _container.Get(type);

            return _container.Get(type, args.Select(arg =>
                new Ninject.Parameters.ConstructorArgument(arg.Name, arg.Value)).ToArray());
        }
    }
}