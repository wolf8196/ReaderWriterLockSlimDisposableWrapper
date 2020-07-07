// <copyright file="WriterGuard.cs" company="Scada International A/S">
// Copyright (c) Scada International A/S. All rights reserved.
// </copyright>

namespace ReaderWriterLockSlimDisposableWrapper
{
    using System;
    using System.Threading;

    public sealed class WriterGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim readerWriterLock;

        public WriterGuard(ReaderWriterLockSlim readerWriterLock)
        {
            this.readerWriterLock = readerWriterLock;
            readerWriterLock.EnterWriteLock();
        }

        public void Dispose()
        {
            readerWriterLock.ExitWriteLock();
        }
    }
}