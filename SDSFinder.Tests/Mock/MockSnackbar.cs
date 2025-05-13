using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace SDSFinder.Testss.Tests.Mock
{
    internal class MockSnackbar : MudBlazor.ISnackbar
    {
        public IEnumerable<Snackbar> ShownSnackbars => throw new NotImplementedException();

        public SnackbarConfiguration Configuration => throw new NotImplementedException();

#pragma warning disable
        public event Action OnSnackbarsUpdated; // Ignore the warning

        public Snackbar Add(string message, Severity severity = Severity.Normal, Action<SnackbarOptions> configure = null)
        {
            throw new NotImplementedException();
        }

        public Snackbar Add(string message, Severity severity = Severity.Normal, Action<SnackbarOptions> configure = null, string key = "")
        {
            throw new NotImplementedException();
        }

        public Snackbar Add(RenderFragment message, Severity severity = Severity.Normal, Action<SnackbarOptions> configure = null, string key = "")
        {
            throw new NotImplementedException();
        }

        public Snackbar Add<T>(Dictionary<string, object> componentParameters = null, Severity severity = Severity.Normal, Action<SnackbarOptions> configure = null, string key = "") where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Snackbar AddNew(Severity severity, string message, Action<SnackbarOptions> configure)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Remove(Snackbar snackbar)
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(string key)
        {
            throw new NotImplementedException();
        }
#pragma warning restore
    }
}
