using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public class Disposable : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected void Dispose(bool disposing)
        {


            if (disposing && !disposed)
            {
                DisposeCore();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        protected virtual void DisposeCore()
        {
            
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }
}
