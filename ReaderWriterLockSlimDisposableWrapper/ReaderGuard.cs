// <copyright file="ReaderGuard.cs" company="Scada International A/S">
// Copyright (c) Scada International A/S. All rights reserved.
// </copyright>

namespace ReaderWriterLockSlimDisposableWrapper
{
    using System;
    using System.Threading;

    public sealed class ReaderGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim readerWriterLock;

        public ReaderGuard(ReaderWriterLockSlim readerWriterLock)
        {
            this.readerWriterLock = readerWriterLock;
            readerWriterLock.EnterReadLock();
        }

        public void Dispose()
        {
            readerWriterLock.ExitReadLock();
        }
    }
}