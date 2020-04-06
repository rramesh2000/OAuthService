using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Behaviours
{
    public abstract class Handler<T> : IHandler<T> where T : class
    {
        private IHandler<T> Next { get; set; }

        public virtual void Handle(T request)
        {
            Next?.Handle(request);
        }

        public IHandler<T> SetNext(IHandler<T> next)
        {
            Next = next;

            return Next;
        }
    }

    public interface IHandler<T> where T : class
    {
        IHandler<T> SetNext(IHandler<T> next);
        void Handle(T request);
    }
}
