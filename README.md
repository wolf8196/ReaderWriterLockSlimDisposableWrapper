# ReaderWriterLockSlimDisposableWrapper
Small and simple disposable wrappers for ReaderWriterLockSlim.

The classes avoid repeating the 'try-Enter-finally-Exit' through the code and reduces the code required to handle ReaderWriterLockSlims, thus helping to keep focus on the part of the code that does the actual work.
# Usage
## Before
```csharp
public class Class1
{
    private ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

    public void Read()
    {
        _readerWriterLock.EnterReadLock();
        try
        {
            // read stuff...
        }
        finally
        {
            _readerWriterLock.ExitReadLock();
        }
    }

    public void Write()
    {
        _readerWriterLock.EnterWriteLock();
        try
        {
            // write stuff...
        }
        finally
        {
            _readerWriterLock.ExitWriteLock();
        }
    }

    public void ReadThenWrite()
    {
        _readerWriterLock.EnterUpgradeableReadLock();
        try
        {
            // get data for condition
            if (condition)
            {
                _readerWriterLock.EnterWriteLock();
                try
                {
                    // write stuff
                }
                finally
                {
                    _readerWriterLock.ExitWriteLock();
                }
            }
        }
        finally
        {
            _readerWriterLock.ExitUpgradeableReadLock();
        }
    }
}
```
## After
```csharp
public class Class1
{
    private ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

    public void Read()
    {
        using (new ReaderGuard(_readerWriterLock))
        {
            // read stuff...
        }
    }

    public void Write()
    {
        using (new WriterGuard(_readerWriterLock))
        {
            // write stuff...
        }
    }

    public void ReadThenWrite()
    {
        using (var upgradeableGuard = new UpgradeableGuard(_readerWriterLock))
        {
            // get data for condition
            if (condition)
            {
                upgradeableGuard.UpgradeToWriterLock(); // optional using can be added
                // write stuff
            }
        }
    }
}
```
# Acknowledgements
All credits for creation goes to [Sebastien GASPAR](https://www.codeproject.com/Members/Geeko37), the author of [ReaderWriterLockSlim and the IDisposable Wrapper](https://www.codeproject.com/Tips/661975/ReaderWriterLockSlim-and-the-IDisposable-Wrapper) on CodeProject.
I only posted this to Github and created a Nuget package, because needed this in multiple projects.
# License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/wolf8196/ReaderWriterLockSlimDisposableWrapper/blob/master/LICENSE) file for details
